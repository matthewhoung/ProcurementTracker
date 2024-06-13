using Domain.Entities.Commons.Generic;
using Domain.Interfaces;
using MediatR;

namespace Application.Generic.Handlers.Queries
{
    public class GetAllWorkerClassesQuery : IRequest<List<WorkerClass>>
    {
    }
        
    public class GetAllWorkerClassesHandler : IRequestHandler<GetAllWorkerClassesQuery, List<WorkerClass>>
    {
        private readonly IGenericRepository _genericRepository;

        public GetAllWorkerClassesHandler(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<List<WorkerClass>> Handle(GetAllWorkerClassesQuery request, CancellationToken cancellationToken)
        {
            return await _genericRepository.GetAllWorkerClassesAsync();
        }
    }
}
