using Application.DTOs;
using Domain.Interfaces;

namespace Application.Services
{
    public class FilteredFormsService
    {
        private readonly IFormRepository _formRepository;
        private readonly FormInfoService _formInfoService;

        public FilteredFormsService(IFormRepository formRepository, FormInfoService formInfoService)
        {
            _formRepository = formRepository;
            _formInfoService = formInfoService;
        }

        public async Task<List<FormInfoDto>> GetAllFormInOneAsync(int? formId, int? userId, string? stage, string? status)
        {
            if (formId.HasValue)
            {
                var formInfo = await _formInfoService.GetFormInfoRecursiveAsync(formId.Value);
                return formInfo != null ? new List<FormInfoDto> { formInfo } : new List<FormInfoDto>();
            }
            else if (userId.HasValue && !string.IsNullOrEmpty(stage) && !string.IsNullOrEmpty(status))
            {
                var formIds = await _formRepository.GetUserFormIdsAsync(userId.Value);
                var formInfoDtos = new List<FormInfoDto>();

                foreach (var formInfoId in formIds)
                {
                    var form = await _formRepository.GetFormByIdAsync(formInfoId);
                    if (form != null && form.Stage == stage)
                    {
                        var formStatus = await _formRepository.GetFormStatusAsync(form.Id);
                        if (formStatus == status)
                        {
                            var formInfo = await _formInfoService.GetFormInfoRecursiveAsync(formInfoId);
                            if (formInfo != null)
                            {
                                formInfoDtos.Add(formInfo);
                            }
                        }
                    }
                }

                return formInfoDtos;
            }
            else if (userId.HasValue)
            {
                var formIds = await _formRepository.GetUserFormIdsAsync(userId.Value);
                var formInfoDtos = new List<FormInfoDto>();

                foreach (var formInfoId in formIds)
                {
                    var formInfo = await _formInfoService.GetFormInfoRecursiveAsync(formInfoId);
                    if (formInfo != null)
                    {
                        formInfoDtos.Add(formInfo);
                    }
                }

                return formInfoDtos;
            }
            else if (!string.IsNullOrEmpty(stage) && !string.IsNullOrEmpty(status))
            {
                var forms = await GetFormsByStageAndStatusAsync(stage, status);
                return forms;
            }
            else
            {
                var forms = await _formRepository.GetAllFormsAsync();
                var formInfoDtos = new List<FormInfoDto>();

                foreach (var form in forms)
                {
                    var formInfo = await _formInfoService.GetFormInfoRecursiveAsync(form.Id);
                    if (formInfo != null)
                    {
                        formInfoDtos.Add(formInfo);
                    }
                }

                return formInfoDtos;
            }
        }

        private async Task<List<FormInfoDto>> GetFormsByStageAndStatusAsync(string stage, string status)
        {
            var forms = await _formRepository.GetAllFormsAsync();

            if (!string.IsNullOrEmpty(stage))
            {
                forms = forms.Where(f => f.Stage == stage).ToList();
            }

            var formInfoDtos = new List<FormInfoDto>();

            foreach (var form in forms)
            {
                var formStatus = await _formRepository.GetFormStatusAsync(form.Id);
                if (formStatus == status)
                {
                    var formInfo = await _formInfoService.GetFormInfoRecursiveAsync(form.Id);
                    if (formInfo != null)
                    {
                        formInfoDtos.Add(formInfo);
                    }
                }
            }

            return formInfoDtos;
        }
    }
}
