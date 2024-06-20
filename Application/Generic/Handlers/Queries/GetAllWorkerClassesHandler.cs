using Domain.Entities.Commons.Generic;
using Domain.Interfaces;
using MediatR;

namespace Application.Generic.Handlers.Queries
{
    public class GetAllWorkersQuery : IRequest<List<Workers>>
    {
    }

    public class GetAllWorkersHandler : IRequestHandler<GetAllWorkersQuery, List<Workers>>
    {
        private readonly IGenericRepository _genericRepository;

        public GetAllWorkersHandler(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<List<Workers>> Handle(GetAllWorkersQuery request, CancellationToken cancellationToken)
        {
            return await _genericRepository.GetAllWorkerClassWithTypesAsync();
        }
    }
}
