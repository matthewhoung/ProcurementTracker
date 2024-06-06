using Domain.Entities.Forms;
using Domain.Interfaces;
using MediatR;

namespace Application.Forms.Handlers.Queries
{
    public class GetFormWorkerByFormIdQuery : IRequest<List<FormWorker>>
    {
        public int FormId { get; set; }

        public GetFormWorkerByFormIdQuery(int formId)
        {
            FormId = formId;
        }
    }
    public class GetFormWorkerByFormIdHandler : IRequestHandler<GetFormWorkerByFormIdQuery, List<FormWorker>>
    {
        private readonly IFormRepository _formRepository;

        public GetFormWorkerByFormIdHandler(IFormRepository formRepository)
        {
            _formRepository = formRepository;
        }
                                                    
        public async Task<List<FormWorker>> Handle(GetFormWorkerByFormIdQuery request, CancellationToken cancellationToken)
        {
            var formWorkers = await _formRepository.GetFormWorkerListByFormIdAsync(request.FormId);
            return formWorkers;
        }
    }
    
}