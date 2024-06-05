using Domain.Entities.Forms;
using MediatR;

namespace Application.Forms.Queries
{
    public class GetFormWorkerByFormIdQuery : IRequest<List<FormWorker>>
    {
        public int FormId { get; set; }

        public GetFormWorkerByFormIdQuery(int formId)
        {
            FormId = formId;
        }
    }
}
