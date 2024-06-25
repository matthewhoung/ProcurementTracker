using Domain.Entities.Forms;
using Domain.Interfaces;
using MediatR;

namespace Application.Forms.Handlers.Queries
{
    public class GetFormCoverDataQuery : IRequest<List<FormCover>>
    {
        public int? UserId { get; set; }
        public string Stage { get; set; }
        public string Status { get; set; }

        public GetFormCoverDataQuery(int? userId, string stage, string status)
        {
            UserId = userId;
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
            var formCoverData = await _formRepository.GetFormCoverDataAsync(request.UserId, request.Stage, request.Status);
            return formCoverData;
        }
    }
}
