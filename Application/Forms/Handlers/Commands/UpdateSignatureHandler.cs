using Domain.Interfaces;
using MediatR;

namespace Application.Forms.Handlers.Commands
{
    public class UpdateSignatureCommand : IRequest<Unit>
    {
        public int FormId { get; set; }
        public int UserId { get; set; }

        public UpdateSignatureCommand(int formId, int userId)
        {
            FormId = formId;
            UserId = userId;
        }
    }

    public class UpdateSignatureCommandHandler : IRequestHandler<UpdateSignatureCommand, Unit>
    {
        private readonly IFormRepository _formRepository;

        public UpdateSignatureCommandHandler(IFormRepository formRepository)
        {
            _formRepository = formRepository;
        }

        public async Task<Unit> Handle(UpdateSignatureCommand request, CancellationToken cancellationToken)
        {
            var formSignatureMember = await _formRepository.GetFormSignatureMemberAsync(request.FormId, request.UserId);

            if (formSignatureMember == null)
            {
                throw new ArgumentException("Signature not found.");
            }

            formSignatureMember.IsChecked = true;

            await _formRepository.UpdateSignatureAsync(formSignatureMember);

            var allChecked = await _formRepository.GetAllSignaturesCheckedAsync(request.FormId, formSignatureMember.Stage);

            if (allChecked)
            {
                if (formSignatureMember.Stage == "OrderForm")
                {
                    // update form stage to ReceiveForm
                    await _formRepository.CreateReceiveFormAsync(request.FormId);
                }
                else if (formSignatureMember.Stage == "ReceiveForm")
                {
                    // update form stage to PayableForm
                    await _formRepository.CreatePayableFormAsync(request.FormId);
                }
            }

            return Unit.Value;
        }
    }
}
