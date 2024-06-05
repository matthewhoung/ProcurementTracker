using Application.Forms.Queries;
using Domain.Entities.Forms;
using Domain.Interfaces;
using MediatR;

namespace Application.Forms.Handlers.Queries
{
    public class GetFormDepartmentByFormIdHandler : IRequestHandler<GetFormDepartmentByFormIdQuery, List<FormDepartment>>
    {
        private readonly IFormRepository _formRepository;

        public GetFormDepartmentByFormIdHandler(IFormRepository formRepository)
        {
            _formRepository = formRepository;
        }

        public async Task<List<FormDepartment>> Handle(GetFormDepartmentByFormIdQuery request, CancellationToken cancellationToken)
        {
            return await _formRepository.GetFormDepartmentsByFormIdAsync(request.FormId);
        }
    }
}
