using Domain.Entities.Commons.Generic;
using Domain.Interfaces;
using MediatR;

namespace Application.Generic.Handlers.Queries
{
    public class GetAllUnitsQuery : IRequest<List<UnitClass>>
    {
    }
    public class GetAllUnitsHandler : IRequestHandler<GetAllUnitsQuery, List<UnitClass>>
    {
        private readonly IGenericRepository _genericRepository;

        public GetAllUnitsHandler(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<List<UnitClass>> Handle(GetAllUnitsQuery request, CancellationToken cancellationToken)
        {
            return await _genericRepository.GetAllUnitsAsync();
        }
    }
}
