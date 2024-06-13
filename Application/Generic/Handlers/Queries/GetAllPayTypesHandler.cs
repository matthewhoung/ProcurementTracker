using Domain.Entities.Commons.Generic;
using Domain.Interfaces;
using MediatR;

namespace Application.Generic.Handlers.Queries
{
    public class GetAllPayTypesQuery : IRequest<List<PayType>>
    {
    }
    public class GetAllPayTypesHandler : IRequestHandler<GetAllPayTypesQuery, List<PayType>>
    {
        private readonly IGenericRepository _genericRepository;

        public GetAllPayTypesHandler(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<List<PayType>> Handle(GetAllPayTypesQuery request, CancellationToken cancellationToken)
        {
            return await _genericRepository.GetAllPayTypesAsync();
        }
    }
}
