using Domain.Entities.Commons.FileUploader;
using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Uploaders.Handlers.Commands
{
    public class CreateFileUploaderCommand : IRequest<int>
    {
        public int FormId { get; set; }
        public int UploaderId { get; set; }
        public IFormFile File { get; set; }
    }
    public class CreateFileUploaderHandler : IRequestHandler<CreateFileUploaderCommand, int>
    {
        private readonly IFileUploaderRepository _fileUploaderRepository;

        public CreateFileUploaderHandler(IFileUploaderRepository fileUploaderRepository)
        {
            _fileUploaderRepository = fileUploaderRepository;
        }

        public async Task<int> Handle(CreateFileUploaderCommand request, CancellationToken cancellationToken)
        {
            var fileUploader = new FileUploader
            {
                FormId = request.FormId,
                UploaderId = request.UploaderId
            };

            var fileId = await _fileUploaderRepository.CreateFileUrlAsync(fileUploader, request.File);
            return fileId;
        }
    }
}
