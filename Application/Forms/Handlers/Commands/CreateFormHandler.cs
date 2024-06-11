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
        public FormPaymentDto PaymentInfo { get; set; }
        public List<FormSignatureMemberDto> FormSignatureMembers { get; set; }
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
            //Base form
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

            //itemDetails
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

            //Workers
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

            //Department
            if (request.FormDepartments != null && request.FormDepartments.Any())
            {
                var formDepartments = request.FormDepartments.Select(d => new FormDepartment
                {
                    FormId = formId,
                    DepartmentId = d.DepartmentId
                }).ToList();

                await _formRepository.CreateFormDepartmentsAsync(formDepartments);
            }

            //PaymentInfo
            if (request.PaymentInfo != null)
            {
                var formPaymentInfo = new FormPayment
                {
                    FormId = formId,
                    PaymentTitleId = request.PaymentInfo.PaymentTitleId,
                    PaymentToolId = request.PaymentInfo.PaymentToolId,
                    PaymentAmount = 0,
                    PaymentDelta = 0,
                    DeltaTitleId = 1,
                    PaymentTotal = 0
                };

                await _formRepository.CreateFormPaymentInfoAsync(formPaymentInfo);
                await _formRepository.UpdatePaymentAmountAsync(formId);
            }

            //SignatureMembers
            if (request.FormSignatureMembers != null && request.FormSignatureMembers.Any())
            {
                var formSignatureMembers = request.FormSignatureMembers.Select(s => new FormSignatureMember
                {
                    FormId = formId,
                    UserId = s.UserId,
                    RoleId = s.RoleId,
                    IsChecked = false
                }).ToList();

                await _formRepository.CreateDefaultSignatureMembersAsync(formSignatureMembers);
            }

            return formId;
        }
    }
}
