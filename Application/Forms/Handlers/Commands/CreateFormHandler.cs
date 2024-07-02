using Application.DTOs;
using Application.Services;
using Domain.Entities.Forms;
using Domain.Interfaces;
using MediatR;
using System.Transactions;

namespace Application.Forms.Handlers.Commands
{
    public class CreateFormCommand : IRequest<int>
    {
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<FormDetailDto> FormDetails { get; set; }
        public FormWorkerDto FormWorker { get; set; }
        public FormDepartmentDto FormDepartment { get; set; }
        public FormPaymentDto PaymentInfo { get; set; }
        public List<FormSignatureMemberDto> FormSignatureMembers { get; set; }
    }

    public class CreateFormHandler : IRequestHandler<CreateFormCommand, int>
    {
        private readonly IFormRepository _formRepository;
        private readonly PaymentCalculationService _paymentCalculationService;

        public CreateFormHandler(IFormRepository formRepository, PaymentCalculationService payment)
        {
            _formRepository = formRepository;
            _paymentCalculationService = payment;
        }

        public async Task<int> Handle(CreateFormCommand request, CancellationToken cancellationToken)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
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
                    int totalPaymentAmount = 0;

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

                        totalPaymentAmount = formDetails.Sum(d => d.Total);

                        await _formRepository.CreateFormDetailsAsync(formDetails);
                    }

                    //PaymentInfo
                    if (request.PaymentInfo != null)
                    {
                        var formPaymentInfo = new FormPayment
                        {
                            FormId = formId,
                            PaymentTitleId = 3,
                            PaymentToolId = request.PaymentInfo.PaymentToolId,
                            IsTaxed = request.PaymentInfo.IsTaxed,
                            IsReceipt = request.PaymentInfo.IsReceipt,
                            IsDelta = request.PaymentInfo.IsDelta,
                            PaymentAmount = request.PaymentInfo.PaymentAmount,
                            PaymentDelta = request.PaymentInfo.PaymentDelta,
                            PaymentTotal = request.PaymentInfo.PaymentTotal,
                            DeltaTitleId = 5,
                            TaxAmount = request.PaymentInfo.TaxAmount
                        };

                        //_paymentCalculationService.CalculatePayment(formPaymentInfo);
                        await _formRepository.CreateFormPaymentInfoAsync(formPaymentInfo);
                    }

                    //Workers

                    if (request.FormWorker != null)
                    {
                        var formWorker = new FormWorker
                        {
                            FormId = formId,
                            WorkerTypeId = request.FormWorker.WorkerTypeId,
                            WorkerTeamId = request.FormWorker.WorkerTeamId
                        };

                        await _formRepository.CreateFormWorkerAsync(formWorker);
                    }

                    //Department
                    if (request.FormDepartment != null)
                    {
                        var formDepartment = new FormDepartment
                        {
                            FormId = formId,
                            DepartmentId = request.FormDepartment.DepartmentId
                        };

                        await _formRepository.CreateFormDepartmentAsync(formDepartment);
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

                    scope.Complete();
                    return formId;
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    throw ex;
                }
            }
        }
    }
}
