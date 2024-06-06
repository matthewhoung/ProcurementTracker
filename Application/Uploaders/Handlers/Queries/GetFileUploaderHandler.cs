using Domain.Entities.Commons.FileUploader;
using Domain.Interfaces;
using MediatR;

namespace Application.Uploaders.Handlers.Queries
{
    public class GetFileUploaderQuery : IRequest<List<FileUploader>>
    {
        public int FormId { get; set; }

        public GetFileUploaderQuery(int formId)
        {
            FormId = formId;
        }
    }
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
