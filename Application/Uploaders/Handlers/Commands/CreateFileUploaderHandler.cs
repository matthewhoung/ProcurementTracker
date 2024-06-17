using Application.Uploaders.DTOs;
using Domain.Entities.Commons.FileUploader;
using Domain.Interfaces;
using MediatR;

namespace Application.Uploaders.Handlers.Commands
{
    public class CreateFileUploaderCommand : IRequest<int>
    {
        public CreateFileUploaderDto FileUploaderDto { get; set; }
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
                FormId = request.FileUploaderDto.FormId,
                UploaderId = request.FileUploaderDto.UploaderId
            };

            var fileId = await _fileUploaderRepository.CreateFileUrlAsync(fileUploader, request.FileUploaderDto.File);
            return fileId;
        }
    }
}
