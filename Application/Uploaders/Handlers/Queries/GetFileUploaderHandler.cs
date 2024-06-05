using Application.Uploaders.Queries;
using Domain.Entities.Commons.FileUploader;
using Domain.Interfaces;
using MediatR;

namespace Application.Uploaders.Handlers.Queries
{
    public class GetFileUploaderHandler : IRequestHandler<GetFileUploaderQuery, List<FileUploader>>
    {
        private readonly IFileUploaderRepository _fileUploaderRepository;

        public GetFileUploaderHandler(IFileUploaderRepository fileUploaderRepository)
        {
            _fileUploaderRepository = fileUploaderRepository;
        }

        public async Task<List<FileUploader>> Handle(GetFileUploaderQuery request, CancellationToken cancellationToken)
        {
            var filePaths = await _fileUploaderRepository.GetFilePathAsync(request.FormId);
            return filePaths;
        }
    }
}
