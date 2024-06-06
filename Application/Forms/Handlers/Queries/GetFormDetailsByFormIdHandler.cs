using Domain.Entities.Forms;
using Domain.Interfaces;
using MediatR;

namespace Application.Forms.Handlers.Queries
{
    public class GetFormDetailsByFormIdQuery : IRequest<List<FormDetail>>
    {
        public int FormId { get; set; }

        public GetFormDetailsByFormIdQuery(int formId)
        {
            FormId = formId;
        }
    }
    public class GetFormDetailsByFormIdHandler : IRequestHandler<GetFormDetailsByFormIdQuery, List<FormDetail>>
    {
        private readonly IFormRepository _formRepository;

        public GetFormDetailsByFormIdHandler(IFormRepository formRepository)
        {
            _formRepository = formRepository;
        }

        public async Task<List<FormDetail>> Handle(GetFormDetailsByFormIdQuery request, CancellationToken cancellationToken)
        {
            var formDetails = await _formRepository.GetFormDetailsByFormIdAsync(request.FormId);
            return formDetails;
        }
    }
}
