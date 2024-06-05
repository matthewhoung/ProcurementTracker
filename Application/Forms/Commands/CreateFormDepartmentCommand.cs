using MediatR;

namespace Application.Forms.Commands
{
    public class CreateFormDepartmentCommand : IRequest<int>
    {
        public int FormId { get; set; }
        public int DepartmentId { get; set; }
    }
}
