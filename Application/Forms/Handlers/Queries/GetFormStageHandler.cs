using Domain.Interfaces;
using MediatR;

namespace Application.Forms.Handlers.Queries
{
    public class GetFormStageQuery : IRequest<string>
    {
        public int FormId { get; set; }

        public GetFormStageQuery(int formId)
        {
            FormId = formId;
        }
    }
    public class GetFormStageHandler : IRequestHandler<GetFormStageQuery, string>
    {
        private readonly IFormRepository _formRepository;

        public GetFormStageHandler(IFormRepository formRepository)
        {
            _formRepository = formRepository;
        }

        public async Task<string> Handle(GetFormStageQuery request, CancellationToken cancellationToken)
        {
            return await _formRepository.GetFormStageAsync(request.FormId);
        }
    }
}
