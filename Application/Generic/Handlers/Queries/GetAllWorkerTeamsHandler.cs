using Domain.Entities.Commons.Generic;
using Domain.Interfaces;
using MediatR;

namespace Application.Generic.Handlers.Queries
{
    public class GetAllWorkerTeamsQuery : IRequest<List<WorkerTeam>>
    {
    }
    public class GetAllWorkerTeamsHandler : IRequestHandler<GetAllWorkerTeamsQuery, List<WorkerTeam>>
    {
        private readonly IGenericRepository _genericRepository;

        public GetAllWorkerTeamsHandler(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<List<WorkerTeam>> Handle(GetAllWorkerTeamsQuery request, CancellationToken cancellationToken)
        {
            return await _genericRepository.GetAllWorkerTeamsAsync();
        }
    }
}
