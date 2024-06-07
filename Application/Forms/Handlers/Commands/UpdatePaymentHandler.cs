using Domain.Entities.Forms;
using Domain.Interfaces;
using MediatR;

namespace Application.Forms.Handlers.Commands
{
    public class UpdatePaymentCalculationsCommand : IRequest<Unit>
    {
        public int FormId { get; }
        public int PaymentDelta { get; }
        public int DeltaTitleId { get; }
        public int PaymentTitleId { get; }
        public int PaymentToolId { get; }

        public UpdatePaymentCalculationsCommand(FormPayment formPayment)
        {
            FormId = formPayment.FormId;
            PaymentDelta = formPayment.PaymentDelta;
            DeltaTitleId = formPayment.DeltaTitleId;
            PaymentTitleId = formPayment.PaymentTitleId;
            PaymentToolId = formPayment.PaymentToolId;
        }
    }

    public class UpdatePaymentCalculationsCommandHandler : IRequestHandler<UpdatePaymentCalculationsCommand, Unit>
    {
        private readonly IFormRepository _formRepository;

        public UpdatePaymentCalculationsCommandHandler(IFormRepository formRepository)
        {
            _formRepository = formRepository;
        }

        public async Task<Unit> Handle(UpdatePaymentCalculationsCommand request, CancellationToken cancellationToken)
        {
            var detailSumTotal = await _formRepository.GetFormDetailSumTotalAsync(request.FormId);

            var paymentTotal = CalculatePaymentTotal(detailSumTotal, request.PaymentDelta, request.DeltaTitleId);

            await _formRepository.UpdatePaymentAsync(request.FormId, request.PaymentDelta, request.DeltaTitleId, request.PaymentTitleId, request.PaymentToolId, detailSumTotal, paymentTotal);

            return Unit.Value;
        }

        private int CalculatePaymentTotal(int paymentAmount, int paymentDelta, int deltaTitleId)
        {
            return deltaTitleId switch
            {
                1 => paymentAmount,
                2 => (int)(paymentAmount * 1.05),
                5 => paymentAmount - paymentDelta,
                7 => paymentAmount + paymentDelta,
                8 => paymentAmount / paymentDelta,
                _ => throw new ArgumentException("Invalid delta title ID")
            };
        }
    }
}
