using Domain.Entities.Commons.FileUploader;
using MediatR;

namespace Application.Uploaders.Queries
{
    public class GetFileUploaderQuery : IRequest<List<FileUploader>>
    {
        public int FormId { get; set; }

        public GetFileUploaderQuery(int formId)
        {
            FormId = formId;
        }
    }
}
