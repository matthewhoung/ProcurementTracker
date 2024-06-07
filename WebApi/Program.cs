using Application;
using Infrastructure;
using Serilog;
using WebApi.Middleware;
using Infrastructure.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMySQLConnection(builder.Configuration);

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.Services
    .AddApplication()
    .AddInfrastructure();

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseMiddleware<ResponseStandardizeMiddleware>();

app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();
