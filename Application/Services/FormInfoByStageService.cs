using Application.DTOs;
using Domain.Interfaces;

namespace Application.Services
{
    public class FormInfoByStageService
    {
        private readonly IFormRepository _formRepository;
        private readonly IFileUploaderRepository _fileUploaderRepository;

        public FormInfoByStageService(IFormRepository formRepository, IFileUploaderRepository fileUploaderRepository)
        {
            _formRepository = formRepository;
            _fileUploaderRepository = fileUploaderRepository;
        }

        public async Task<FormInfoDto> GetFormInfoByStageRecursiveAsync(int formId, string stage)
        {
            var form = await _formRepository.GetFormByIdAsync(formId);
            if (form == null) return null;

            var details = await _formRepository.GetFormDetailsByFormIdAsync(formId);
            var payments = await _formRepository.GetFormPaymentInfoByFormIdAsync(formId);
            var workers = await _formRepository.GetFormWorkerByFormIdAsync(formId);
            var departments = await _formRepository.GetFormDepartmentsByFormIdAsync(formId);
            var signatures = await _formRepository.GetFormSignatureByFormIdAndStageAsync(formId, stage);
            var filePaths = await _fileUploaderRepository.GetFilePathAsync(formId);
            var affiliateForms = await _formRepository.GetAllAffiliateFormsAsync(formId);

            var formInfoDto = new FormInfoDto
            {
                Id = form.Id,
                SerialNumber = form.SerialNumber,
                ProjectId = form.ProjectId,
                ProjectName = form.ProjectName,
                Title = form.Title,
                Description = form.Description,
                Stage = stage,
                PaymentTotal = payments.FirstOrDefault()?.PaymentTotal ?? 0,
                Status = signatures.FirstOrDefault().Status,
                CreatedAt = form.CreatedAt,
                UpdatedAt = form.UpdatedAt,
                Details = details,
                Signatures = signatures,
                Workers = workers,
                Payments = payments,
                Departments = departments,
                FilePaths = filePaths,
                Affiliates = new List<FormAffiliateDto>()
            };

            var affiliateFormIds = affiliateForms.Select(af => af.AffiliateFormId).ToList();
            var affiliateInfos = await GetFormInfosByIdsAsync(affiliateFormIds, stage);

            foreach (var affiliateForm in affiliateForms)
            {
                var affiliateFormInfo = affiliateInfos.FirstOrDefault(info => info.Id == affiliateForm.AffiliateFormId);
                formInfoDto.Affiliates.Add(new FormAffiliateDto
                {
                    FormId = affiliateForm.FormId,
                    AffiliateFormId = affiliateForm.AffiliateFormId,
                    AffiliateFormInfo = affiliateFormInfo
                });
            }

            return formInfoDto;
        }

        private async Task<List<FormInfoDto>> GetFormInfosByIdsAsync(IEnumerable<int> formIds, string stage)
        {
            var formInfos = new List<FormInfoDto>();
            foreach (var formId in formIds)
            {
                var formInfo = await GetFormInfoByStageRecursiveAsync(formId, stage);
                if (formInfo != null)
                {
                    formInfos.Add(formInfo);
                }
            }

            return formInfos;
        }
    }
}
