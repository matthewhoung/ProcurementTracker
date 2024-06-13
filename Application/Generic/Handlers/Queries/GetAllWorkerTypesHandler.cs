using Domain.Entities.Commons.Generic;
using Domain.Interfaces;
using MediatR;

namespace Application.Generic.Handlers.Queries
{
    public class GetAllWorkerTypesQuery : IRequest<List<WorkerType>>
    {
    }
    public class GetAllWorkerTypesHandler : IRequestHandler<GetAllWorkerTypesQuery, List<WorkerType>>
    {
        private readonly IGenericRepository _genericRepository;

        public GetAllWorkerTypesHandler(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<List<WorkerType>> Handle(GetAllWorkerTypesQuery request, CancellationToken cancellationToken)
        {
            return await _genericRepository.GetAllWorkerTypesAsync();
        }
    }
}
