using Domain.Entities.Forms;
using Domain.Interfaces;
using MediatR;

namespace Application.Forms.Handlers.Commands
{
    public class CreateFormWorkerCommand : IRequest<int>
    {
        public int FormId { get; set; }
        public int WorkerTypeId { get; set; }
        public int WorkerTeamId { get; set; }
    }
    public class CreateFormWorkerHandler : IRequestHandler<CreateFormWorkerCommand, int>
    {
        private readonly IFormRepository _formRepository;

        public CreateFormWorkerHandler(IFormRepository formRepository)
        {
            _formRepository = formRepository;
        }
        public async Task<int> Handle(CreateFormWorkerCommand request, CancellationToken cancellationToken)
        {
            var formWorker = new FormWorker
            {
                FormId = request.FormId,
                WorkerTypeId = request.WorkerTypeId,
                WorkerTeamId = request.WorkerTeamId
            };

            var formWorkerId = await _formRepository.CreateFormWorkerList(formWorker);
            return formWorkerId;
        }
    }
}
