using MediatR;

namespace Application.Forms.Commands
{
    public class CreateFormPaymentCommand : IRequest<int>
    {
        public int FormId { get; set; }
        public int PaymentTitleId { get; set; }
        public int PaymentToolId { get; set; }
    }
}
