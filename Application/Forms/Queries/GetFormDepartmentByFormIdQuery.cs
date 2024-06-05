using Domain.Entities.Forms;
using MediatR;

namespace Application.Forms.Queries
{
    public class GetFormDepartmentByFormIdQuery : IRequest<List<FormDepartment>>
    {
        public int FormId { get; set; }

        public GetFormDepartmentByFormIdQuery(int formId)
        {
            FormId = formId;
        }
    }
}
