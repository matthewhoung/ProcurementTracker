using Domain.Entities.Forms;
using Domain.Interfaces;
using MediatR;

namespace Application.Forms.Handlers.Commands
{
    public class CreateFormPaymentCommand : IRequest<int>
    {
        public int FormId { get; set; }
        public int PaymentTitleId { get; set; }
        public int PaymentToolId { get; set; }
        public int PaymentDelta { get; set; }
        public int IsTaxed { get; set; }
        public int IsReceipt { get; set; }
    }
    public class CreateFormPaymentHandler : IRequestHandler<CreateFormPaymentCommand, int>
    {
        private readonly IFormRepository _formRepository;

        public CreateFormPaymentHandler(IFormRepository formRepository)
        {
            _formRepository = formRepository;
        }

        public async Task<int> Handle(CreateFormPaymentCommand request, CancellationToken cancellationToken)
        {
            var formPayment = new FormPayment
            {
                FormId = request.FormId,
                PaymentTitleId = request.PaymentTitleId,
                PaymentToolId = request.PaymentToolId,
                IsTaxed = request.IsTaxed,
                IsReceipt = request.IsReceipt
            };

            var formPaymentId = await _formRepository.CreateFormPaymentInfoAsync(formPayment);
            return formPaymentId;
        }
    }
}
