using MediatR;

namespace Application.Forms.Commands
{
    public class CreateFormSignatureMemberCommand : IRequest<int>
    {
        public int FormId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}
