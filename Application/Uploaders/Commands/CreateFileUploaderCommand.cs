using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Uploaders.Commands
{
    public class CreateFileUploaderCommand : IRequest<int>
    {
        public int FormId { get; set; }
        public int UploaderId { get; set; }
        public IFormFile File { get; set; }
    }
}
