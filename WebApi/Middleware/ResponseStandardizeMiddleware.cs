using Newtonsoft.Json;
using System.Text;

namespace WebApi.Middleware
{
    public class ResponseStandardizeMiddleware
    {
        private readonly RequestDelegate _next;

        public ResponseStandardizeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var originalBodyStream = context.Response.Body;
            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;

                await _next(context);

                context.Response.Body = originalBodyStream;

                var responseContent = await FormatResponse(responseBody, context.Response.StatusCode);

                context.Response.ContentType = "application/json";
                context.Response.ContentLength = Encoding.UTF8.GetByteCount(responseContent);
                await context.Response.WriteAsync(responseContent);
            }
        }

        private async Task<string> FormatResponse(MemoryStream responseBody, int statusCode)
        {
            responseBody.Seek(0, SeekOrigin.Begin);
            var bodyAsText = await new StreamReader(responseBody).ReadToEndAsync();

            object data;
            try
            {
                data = JsonConvert.DeserializeObject(bodyAsText);
            }
            catch (JsonReaderException)
            {
                data = bodyAsText; // Return the raw text if it's not valid JSON
            }

            var responseObj = new ResponseBody
            {
                code = statusCode.ToString(),
                message = GetStatusMessage(statusCode),
                data = data
            };

            return JsonConvert.SerializeObject(responseObj);
        }

        private string GetStatusMessage(int statusCode)
        {
            return statusCode switch
            {
                200 => "Success",
                201 => "Created",
                204 => "No Content",
                400 => "Bad Request",
                401 => "Unauthorized",
                403 => "Forbidden",
                404 => "Not Found",
                500 => "Internal Server Error",
                _ => "Unknown"
            };
        }
    }

    public class ResponseBody
    {
        public string code { get; set; }
        public string message { get; set; }
        public object data { get; set; }
    }
}
