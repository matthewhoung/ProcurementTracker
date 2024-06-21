using Application.Uploaders.DTOs;
using Domain.Entities.Commons.FileUploader;
using Domain.Interfaces;
using MediatR;

namespace Application.Uploaders.Handlers.Commands
{
    public class CreateFileUploaderCommand : IRequest<List<int>>
    {
        public CreateFileUploaderDto FileUploaderDto { get; set; }
    }

    public class CreateFileUploaderHandler : IRequestHandler<CreateFileUploaderCommand, List<int>>
    {
        private readonly IFileUploaderRepository _fileUploaderRepository;

        public CreateFileUploaderHandler(IFileUploaderRepository fileUploaderRepository)
        {
            _fileUploaderRepository = fileUploaderRepository;
        }

        public async Task<List<int>> Handle(CreateFileUploaderCommand request, CancellationToken cancellationToken)
        {
            var fileUploader = new FileUploader
            {
                FormId = request.FileUploaderDto.FormId,
                UploaderId = request.FileUploaderDto.UploaderId
            };

            var fileIds = await _fileUploaderRepository.CreateFileUrlsAsync(fileUploader, request.FileUploaderDto.Files);
            return fileIds;
        }
    }
}
