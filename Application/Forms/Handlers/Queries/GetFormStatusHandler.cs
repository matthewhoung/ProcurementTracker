using Domain.Interfaces;
using MediatR;

namespace Application.Forms.Handlers.Queries
{
    public class GetFormStatusQuery : IRequest<string>
    {
        public int FormId { get; set; }

        public GetFormStatusQuery(int formId)
        {
            FormId = formId;
        }
    }
    public class GetFormStatusHandler : IRequestHandler<GetFormStatusQuery, string>
    {
        private readonly IFormRepository _formRepository;

        public GetFormStatusHandler(IFormRepository formRepository)
        {
            _formRepository = formRepository;
        }

        public async Task<string> Handle(GetFormStatusQuery request, CancellationToken cancellationToken)
        {
            var formStatus = await _formRepository.GetFormStatusAsync(request.FormId);
            return formStatus;
        }
    }
}
