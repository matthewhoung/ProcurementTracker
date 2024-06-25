using Application.DTOs;
using Application.Services;
using MediatR;

namespace Application.Forms.Handlers.Queries
{
    public class GetFormByIdStageQuery : IRequest<FormInfoDto>
    {
        public int FormId { get; }
        public string Stage { get; }

        public GetFormByIdStageQuery(int formId, string stage)
        {
            FormId = formId;
            Stage = stage;
        }
    }
    public class GetFormByIdStageHandler : IRequestHandler<GetFormByIdStageQuery, FormInfoDto>
    {
        private readonly FormInfoByStageService _formInfoByStageService;

        public GetFormByIdStageHandler(FormInfoByStageService formInfoByStageService)
        {
            _formInfoByStageService = formInfoByStageService;
        }

        public async Task<FormInfoDto> Handle(GetFormByIdStageQuery request, CancellationToken cancellationToken)
        {
            return await _formInfoByStageService.GetFormInfoByStageRecursiveAsync(request.FormId, request.Stage);
        }
    }
}
