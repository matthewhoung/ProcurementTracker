using Domain.Entities.Forms;
using Domain.Interfaces;
using MediatR;

namespace Application.Forms.Handlers.Queries
{
    public class GetFormStatusCountsQuery : IRequest<List<FormStatusCount>>
    {
        public string Stage { get; }

        public GetFormStatusCountsQuery(string stage)
        {
            Stage = stage;
        }
    }

    public class GetFormStatusCountsHandler : IRequestHandler<GetFormStatusCountsQuery, List<FormStatusCount>>
    {
        private readonly IFormRepository _formRepository;

        public GetFormStatusCountsHandler(IFormRepository formRepository)
        {
            _formRepository = formRepository;
        }

        public async Task<List<FormStatusCount>> Handle(GetFormStatusCountsQuery request, CancellationToken cancellationToken)
        {
            var statusCounts = await _formRepository.GetFormStatusCountsAsync(request.Stage);
            return statusCounts;
        }
    }
}
