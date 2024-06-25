using Domain.Entities.Forms;
using Domain.Interfaces;
using MediatR;

namespace Application.Forms.Handlers.Queries
{
    public class GetFormWorkerByFormIdQuery : IRequest<FormWorker>
    {
        public int FormId { get; set; }

        public GetFormWorkerByFormIdQuery(int formId)
        {
            FormId = formId;
        }
    }
    public class GetFormWorkerByFormIdHandler : IRequestHandler<GetFormWorkerByFormIdQuery, FormWorker>
    {
        private readonly IFormRepository _formRepository;

        public GetFormWorkerByFormIdHandler(IFormRepository formRepository)
        {
            _formRepository = formRepository;
        }
                                                    
        public async Task<FormWorker> Handle(GetFormWorkerByFormIdQuery request, CancellationToken cancellationToken)
        {
            var formWorkers = await _formRepository.GetFormWorkerByFormIdAsync(request.FormId);
            return formWorkers;
        }
    }
    
}