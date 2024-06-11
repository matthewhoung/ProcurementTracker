using Application.DTOs;
using Domain.Entities.Forms;
using Domain.Interfaces;
using MediatR;

namespace Application.Forms.Handlers.Commands
{
    public class CreateFormCommand : IRequest<int>
    {
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<FormDetailDto> FormDetails { get; set; }
        public List<FormWorkerDto> FormWorkers { get; set; }
        public List<FormDepartmentDto> FormDepartments { get; set; }
    }

    public class CreateFormHandler : IRequestHandler<CreateFormCommand, int>
    {
        private readonly IFormRepository _formRepository;

        public CreateFormHandler(IFormRepository formRepository)
        {
            _formRepository = formRepository;
        }

        public async Task<int> Handle(CreateFormCommand request, CancellationToken cancellationToken)
        {
            var form = new Form
            {
                ProjectId = request.ProjectId,
                Title = request.Title,
                Description = request.Description,
                Stage = "OrderForm",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var formId = await _formRepository.CreateFormAsync(form);

            if (formId == 0)
            {
                throw new Exception("Failed to create form");
            }

            // Map FormDetailsDto to FormDetail and set default values
            if (request.FormDetails != null && request.FormDetails.Any())
            {
                var formDetails = request.FormDetails.Select(d => new FormDetail
                {
                    FormId = formId,
                    Title = d.Title,
                    Description = d.Description,
                    Quantity = d.Quantity,
                    UnitPrice = d.UnitPrice,
                    UnitId = d.UnitId,
                    Total = d.Quantity * d.UnitPrice,
                    IsChecked = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }).ToList();

                await _formRepository.CreateFormDetailsAsync(formDetails);
            }

            // Map FormWorkersDto to FormWorker and set FormId
            if (request.FormWorkers != null && request.FormWorkers.Any())
            {
                var formWorkers = request.FormWorkers.Select(w => new FormWorker
                {
                    FormId = formId,
                    WorkerTypeId = w.WorkerTypeId,
                    WorkerTeamId = w.WorkerTeamId
                }).ToList();

                await _formRepository.CreateFormWorkersAsync(formWorkers);
            }

            // Map FormDepartmentsDto to FormDepartment and set FormId
            if (request.FormDepartments != null && request.FormDepartments.Any())
            {
                var formDepartments = request.FormDepartments.Select(d => new FormDepartment
                {
                    FormId = formId,
                    DepartmentId = d.DepartmentId
                }).ToList();

                await _formRepository.CreateFormDepartmentsAsync(formDepartments);
            }

            return formId;
        }
    }
}
