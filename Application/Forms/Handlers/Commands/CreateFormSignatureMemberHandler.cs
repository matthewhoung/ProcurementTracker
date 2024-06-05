using Application.Forms.Commands;
using Domain.Entities.Forms;
using Domain.Interfaces;
using MediatR;

namespace Application.Forms.Handlers.Commands
{
    public class CreateFormSignatureMemberHandler : IRequestHandler<CreateFormSignatureMemberCommand, int>
    {
        private readonly IFormRepository _formRepository;

        public CreateFormSignatureMemberHandler(IFormRepository formRepository)
        {
            _formRepository = formRepository;
        }

        public async Task<int> Handle(CreateFormSignatureMemberCommand request, CancellationToken cancellationToken)
        {
            var formSignatureMember = new FormSignatureMember
            {
                FormId = request.FormId,
                UserId = request.UserId,
                RoleId = request.RoleId,
                IsChecked = false
            };

            var formSignatureMemberId = await _formRepository.CreateFormSignatureMemberAsync(formSignatureMember);
            return formSignatureMemberId;
        }
    }
}
