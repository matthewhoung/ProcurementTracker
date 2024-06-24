using Domain.Entities.Forms;
using Domain.Interfaces;
using FluentValidation;
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
        private readonly IValidator<UpdateSignatureCommand> _validator;

        public UpdateSignatureCommandHandler(IFormRepository formRepository, IValidator<UpdateSignatureCommand> validator)
        {
            _formRepository = formRepository;
            _validator = validator;
        }

        public async Task<Unit> Handle(UpdateSignatureCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var formSignatureMembers = await _formRepository.GetFormSignatureMembersByFormIdAsync(request.FormId);
            var formSignatureMember = formSignatureMembers.FirstOrDefault(member => member.UserId == request.UserId);

            if (formSignatureMember == null)
            {
                throw new ArgumentException("Signature not found.");
            }

            formSignatureMember.IsChecked = true;
            await _formRepository.UpdateSignatureAsync(formSignatureMember);

            var formStatus = await _formRepository.GetFormStatusAsync(request.FormId);
            await _formRepository.UpdateFormStatusAsync(request.FormId, formSignatureMember.Stage, formStatus);

            if (formStatus == "finished")
            {
                await HandleFormCompletetionAsync(request.FormId, formSignatureMember.Stage, formSignatureMembers);
            }

            return Unit.Value;
        }

        private async Task HandleFormCompletetionAsync(int formId, string currentstage, List<FormSignatureMember> currentSignatures)
        {
            if (currentstage == "OrderForm")
            {
                await _formRepository.CreateReceiveFormAsync(formId);
                await _formRepository.UpdateFormStageAsync(formId, "ReceiveForm");
                await CreateSignaturesForNextStage(formId, "ReceiveForm", currentSignatures);
            }
            else if (currentstage == "ReceiveForm")
            {
                await _formRepository.CreatePayableFormAsync(formId);
                await _formRepository.UpdateFormStageAsync(formId, "PayableForm");
                await CreateSignaturesForNextStage(formId, "PayableForm", currentSignatures);
            }
        }

        private async Task CreateSignaturesForNextStage(int formId, string newStage, List<FormSignatureMember> currentSignatures)
        {
            foreach (var signature in currentSignatures)
            {
                var newSignature = new FormSignatureMember
                {
                    FormId = formId,
                    UserId = signature.UserId,
                    RoleId = signature.RoleId,
                    Stage = newStage,
                    IsChecked = false
                };

                await _formRepository.CreateFormSignatureMemberAsync(newSignature);
            }
        }
    }
}
