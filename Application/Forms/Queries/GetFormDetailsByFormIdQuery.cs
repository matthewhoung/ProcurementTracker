using Domain.Entities.Forms;
using MediatR;

namespace Application.Forms.Queries
{
    public class GetFormDetailsByFormIdQuery : IRequest<List<FormDetail>>
    {
        public int FormId { get; set; }

        public GetFormDetailsByFormIdQuery(int formId)
        {
            FormId = formId;
        }
    }
}
