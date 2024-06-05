using Domain.Entities.Forms;
using MediatR;

namespace Application.Forms.Queries
{
    public class GetFormPaymentByFormIdQuery : IRequest<List<FormPayment>>
    {
        public int FormId { get; set; }

        public GetFormPaymentByFormIdQuery(int formId)
        {
            FormId = formId;
        }
    }
}
