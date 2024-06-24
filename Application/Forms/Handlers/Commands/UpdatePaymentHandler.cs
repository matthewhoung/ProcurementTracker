using Application.DTOs;
using Domain.Entities.Forms;
using Domain.Interfaces;
using MediatR;

namespace Application.Forms.Handlers.Commands
{
    public class UpdatePaymentCalculationsCommand : IRequest<Unit>
    {
        public UpdatePaymentCalculationsDto PaymentCalculationsDto { get; }

        public UpdatePaymentCalculationsCommand(UpdatePaymentCalculationsDto paymentCalculationsDto)
        {
            PaymentCalculationsDto = paymentCalculationsDto;
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
            var dto = request.PaymentCalculationsDto;

            var detailSumTotal = await _formRepository.GetFormDetailSumTotalAsync(dto.FormId);

            var paymentTotal = CalculatePaymentTotal(detailSumTotal, dto.PaymentDelta, dto.DeltaTitleId);

            var formPayment = new FormPayment
            {
                FormId = dto.FormId,
                PaymentDelta = dto.PaymentDelta,
                DeltaTitleId = dto.DeltaTitleId,
                PaymentTotal = paymentTotal,
                PaymentAmount = detailSumTotal,
                PaymentTitleId = dto.PaymentTitleId,
                PaymentToolId = dto.PaymentToolId
            };

            await _formRepository.UpdatePaymentAsync(formPayment);

            return Unit.Value;
        }

        private int CalculatePaymentTotal(int paymentAmount, int paymentDelta, int deltaTitleId)
        {
            return deltaTitleId switch
            {
                1 => paymentAmount,
                2 => (int)(paymentAmount * 1.05),
                5 => paymentAmount - paymentDelta,
                7 => paymentAmount - paymentDelta,
                _ => throw new ArgumentException("Invalid delta title ID")
            };
        }
    }
}
