using Application.DTOs;
using Application.Services;
using MediatR;

namespace Application.Forms.Handlers.Queries
{
    public class GetFormByIdQuery : IRequest<FormInfoDto>
    {
        public int FormId { get; set; }

        public GetFormByIdQuery(int formId)
        {
            FormId = formId;
        }
    }

    public class GetFormByIdHandler : IRequestHandler<GetFormByIdQuery, FormInfoDto>
    {
        private readonly FormInfoService _formService;

        public GetFormByIdHandler(FormInfoService formService)
        {
            _formService = formService;
        }

        public async Task<FormInfoDto> Handle(GetFormByIdQuery request, CancellationToken cancellationToken)
        {
            return await _formService.GetFormInfoRecursiveAsync(request.FormId);
        }
    }
}
