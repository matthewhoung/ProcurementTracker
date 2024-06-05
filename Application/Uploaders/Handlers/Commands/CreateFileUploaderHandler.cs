using Application.Uploaders.Commands;
using Domain.Entities.Commons.FileUploader;
using Domain.Interfaces;
using MediatR;

namespace Application.Uploaders.Handlers.Commands
{
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
