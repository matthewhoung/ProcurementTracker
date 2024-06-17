using Microsoft.AspNetCore.Http;

namespace Application.Uploaders.DTOs
{
    public class CreateFileUploaderDto
    {
        public int FormId { get; set; }
        public int UploaderId { get; set; }
        public IFormFile File { get; set; }
    }
}
