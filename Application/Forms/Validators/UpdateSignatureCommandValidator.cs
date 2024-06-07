using Application.Forms.Handlers.Commands;
using Domain.Interfaces;
using FluentValidation;

namespace Application.Forms.Validators
{
    public class UpdateSignatureCommandValidator : AbstractValidator<UpdateSignatureCommand>
    {
        private readonly IFormRepository _formRepository;

        public UpdateSignatureCommandValidator(IFormRepository formRepository)
        {
            _formRepository = formRepository;

            RuleFor(command => command.UserId)
                .MustAsync(UserIdMatchesRole)
                .WithMessage("The user ID does not match the user ID for the role.");
        }

        private async Task<bool> UserIdMatchesRole(UpdateSignatureCommand command, int userId, CancellationToken cancellationToken)
        {
            var formSignatureMembers = await _formRepository.GetFormSignatureMembersByFormIdAsync(command.FormId);
            var formSignatureMember = formSignatureMembers.FirstOrDefault(member => member.UserId == userId);
            return formSignatureMember != null;
        }
    }
}
