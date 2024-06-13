using Domain.Entities.Commons.Generic;
using Domain.Interfaces;
using MediatR;

namespace Application.Generic.Handlers.Queries
{
    public class GetAllRolesQuery : IRequest<List<Roles>>
    {
    }
    public class GetAllRolesHandler : IRequestHandler<GetAllRolesQuery, List<Roles>>
    {
        private readonly IGenericRepository _genericRepository;

        public GetAllRolesHandler(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<List<Roles>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            return await _genericRepository.GetAllRolesAsync();
        }
    }
}
