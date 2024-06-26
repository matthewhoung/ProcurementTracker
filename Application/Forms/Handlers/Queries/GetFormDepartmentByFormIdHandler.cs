using Domain.Entities.Forms;
using Domain.Interfaces;
using MediatR;

namespace Application.Forms.Handlers.Queries
{
    public class GetFormDepartmentByFormIdQuery : IRequest<FormDepartment>
    {
        public int FormId { get; set; }

        public GetFormDepartmentByFormIdQuery(int formId)
        {
            FormId = formId;
        }
    }
    public class GetFormDepartmentByFormIdHandler : IRequestHandler<GetFormDepartmentByFormIdQuery, FormDepartment>
    {
        private readonly IFormRepository _formRepository;

        public GetFormDepartmentByFormIdHandler(IFormRepository formRepository)
        {
            _formRepository = formRepository;
        }

        public async Task<FormDepartment> Handle(GetFormDepartmentByFormIdQuery request, CancellationToken cancellationToken)
        {
            return await _formRepository.GetFormDepartmentsByFormIdAsync(request.FormId);
        }
    }
}
