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
                if (formSignatureMember.Stage == "OrderForm")
                {
                    await _formRepository.CreateReceiveFormAsync(request.FormId);
                    await _formRepository.UpdateFormStageAsync(request.FormId, "ReceiveForm");
                    await CreateSignaturesForNextStage(request.FormId, "ReceiveForm", formSignatureMembers);
                }
                else if (formSignatureMember.Stage == "ReceiveForm")
                {
                    await _formRepository.CreatePayableFormAsync(request.FormId);
                    await _formRepository.UpdateFormStageAsync(request.FormId, "PayableForm");
                    await CreateSignaturesForNextStage(request.FormId, "PayableForm", formSignatureMembers);
                }
            }

            return Unit.Value;
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
