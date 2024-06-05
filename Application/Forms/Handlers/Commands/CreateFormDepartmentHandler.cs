using Application.Forms.Commands;
using Domain.Entities.Forms;
using Domain.Interfaces;
using MediatR;

namespace Application.Forms.Handlers.Commands
{
    public class CreateFormDepartmentHandler : IRequestHandler<CreateFormDepartmentCommand, int>
    {
        private readonly IFormRepository _formRepository;

        public CreateFormDepartmentHandler(IFormRepository formRepository)
        {
            _formRepository = formRepository;
        }

        public async Task<int> Handle(CreateFormDepartmentCommand request, CancellationToken cancellationToken)
        {
            var formDepartment = new FormDepartment
            {
                FormId = request.FormId,
                DepartmentId = request.DepartmentId,
            };

            var formDepartmentId = await _formRepository.CreateFormDepartment(formDepartment);
            return formDepartmentId;
        }
    }
}
