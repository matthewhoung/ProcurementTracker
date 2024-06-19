using Application.DTOs;
using Application.Services;
using Domain.Interfaces;
using MediatR;

namespace Application.Forms.Handlers.Queries
{
    public class GetAllFormsQuery : IRequest<List<FormInfoDto>>
    {
    }

    public class GetAllFormsHandler : IRequestHandler<GetAllFormsQuery, List<FormInfoDto>>
    {
        private readonly IFormRepository _formRepository;
        private readonly FormInfoService _formService;

        public GetAllFormsHandler(IFormRepository formRepository, FormInfoService formService)
        {
            _formRepository = formRepository;
            _formService = formService;
        }

        public async Task<List<FormInfoDto>> Handle(GetAllFormsQuery request, CancellationToken cancellationToken)
        {
            var forms = await _formRepository.GetAllFormsAsync();
            var formInfoDtos = new List<FormInfoDto>();

            foreach (var form in forms)
            {
                var formInfo = await _formService.GetFormInfoRecursiveAsync(form.Id);
                if (formInfo != null)
                {
                    formInfoDtos.Add(formInfo);
                }
            }

            return formInfoDtos;
        }
    }
}
