using Domain.Entities.Commons.Generic;
using Domain.Interfaces;
using MediatR;

namespace Application.Generic.Handlers.Commands
{
    public class CreateWorkerTeamCommand : IRequest<int>
    {
        public WorkerTeam WorkerTeam { get; set; }
    }
    public class CreateWorkerTeamHandler : IRequestHandler<CreateWorkerTeamCommand, int>
    {
        private readonly IGenericRepository _genericRepository;

        public CreateWorkerTeamHandler(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<int> Handle(CreateWorkerTeamCommand request, CancellationToken cancellationToken)
        {
            await _genericRepository.CreateWorkerTeamAsync(request.WorkerTeam);
            return request.WorkerTeam.WorkerTeamId;
        }
    }
}
