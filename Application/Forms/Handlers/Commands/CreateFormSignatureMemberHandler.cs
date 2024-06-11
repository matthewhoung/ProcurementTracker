using Domain.Entities.Forms;
using Domain.Interfaces;
using MediatR;

namespace Application.Forms.Handlers.Commands
{
    public class CreateFormSignatureMemberCommand : IRequest<int>
    {
        public int FormId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public bool IsChecked { get; set; }
        //public string Stage { get; set; }
    }
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
                Stage = "OrderForm",
                IsChecked = request.IsChecked
            };

            var signId = await _formRepository.CreateFormSignatureMemberAsync(formSignatureMember);
            return signId;
        }
    }
}
