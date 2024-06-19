using Application.DTOs;
using Domain.Interfaces;

namespace Application.Services
{
    public class FormInfoService
    {
        private readonly IFormRepository _formRepository;
        private readonly IFileUploaderRepository _fileUploaderRepository;

        public FormInfoService(IFormRepository formRepository, IFileUploaderRepository fileUploaderRepository)
        {
            _formRepository = formRepository;
            _fileUploaderRepository = fileUploaderRepository;
        }

        public async Task<FormInfoDto> GetFormInfoRecursiveAsync(int formId)
        {
            var form = await _formRepository.GetFormByIdAsync(formId);
            if (form == null) return null;

            var details = await _formRepository.GetFormDetailsByFormIdAsync(formId);
            var payments = await _formRepository.GetFormPaymentInfoByFormIdAsync(formId);
            var workers = await _formRepository.GetFormWorkerListByFormIdAsync(formId);
            var departments = await _formRepository.GetFormDepartmentsByFormIdAsync(formId);
            var signatures = await _formRepository.GetFormSignatureMembersByFormIdAsync(formId);
            var filePaths = await _fileUploaderRepository.GetFilePathAsync(formId);
            var status = await _formRepository.GetFormStatusAsync(formId);
            var affiliateForms = await _formRepository.GetAllAffiliateFormsAsync(formId);

            var formInfoDto = new FormInfoDto
            {
                Id = form.Id,
                ProjectId = form.ProjectId,
                ProjectName = form.ProjectName,
                Title = form.Title,
                Description = form.Description,
                Stage = form.Stage,
                Status = status,
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

            foreach (var affiliateForm in affiliateForms)
            {
                var affiliateFormInfo = await GetFormInfoRecursiveAsync(affiliateForm.AffiliateFormId);
                formInfoDto.Affiliates.Add(new FormAffiliateDto
                {
                    FormId = affiliateForm.FormId,
                    AffiliateFormId = affiliateForm.AffiliateFormId,
                    AffiliateFormInfo = affiliateFormInfo
                });
            }

            return formInfoDto;
        }
    }
}
