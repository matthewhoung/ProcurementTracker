using Domain.Entities.Forms;
using Domain.Interfaces;
using MediatR;

namespace Application.Forms.Handlers.Queries
{
    public class GetFormCoverDataQuery : IRequest<List<FormCover>>
    {
        public string Stage { get; set; }
        public string Status { get; set; }

        public GetFormCoverDataQuery(string stage, string status)
        {
            Stage = stage;
            Status = status;
        }
    }

    public class GetFormCoverDataHandler : IRequestHandler<GetFormCoverDataQuery, List<FormCover>>
    {
        private readonly IFormRepository _formRepository;

        public GetFormCoverDataHandler(IFormRepository formRepository)
        {
            _formRepository = formRepository;
        }

        public async Task<List<FormCover>> Handle(GetFormCoverDataQuery request, CancellationToken cancellationToken)
        {
            var formCoverData = await _formRepository.GetFormCoverDataAsync(request.Stage, request.Status);
            return formCoverData;
        }
    }
}
