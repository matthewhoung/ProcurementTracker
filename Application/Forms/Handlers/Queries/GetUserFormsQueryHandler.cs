using Application.DTOs;
using Application.Services;
using Domain.Interfaces;
using MediatR;

namespace Application.Forms.Handlers.Queries
{
    public class GetUserFormsQuery : IRequest<IEnumerable<FormInfoDto>>
    {
        public int UserId { get; set; }

        public GetUserFormsQuery(int userId)
        {
            UserId = userId;
        }
    }

    public class GetUserFormsQueryHandler : IRequestHandler<GetUserFormsQuery, IEnumerable<FormInfoDto>>
    {
        private readonly IFormRepository _formRepository;
        private readonly FormService _formService;

        public GetUserFormsQueryHandler(IFormRepository formRepository, FormService formService)
        {
            _formRepository = formRepository;
            _formService = formService;
        }

        public async Task<IEnumerable<FormInfoDto>> Handle(GetUserFormsQuery request, CancellationToken cancellationToken)
        {
            var formIds = await _formRepository.GetUserFormIdsAsync(request.UserId);
            var formInfoDtos = new List<FormInfoDto>();

            foreach (var formId in formIds)
            {
                var formInfoDto = await _formService.GetFormInfoRecursiveAsync(formId);
                if (formInfoDto != null)
                {
                    formInfoDtos.Add(formInfoDto);
                }
            }

            return formInfoDtos;
        }
    }
}
