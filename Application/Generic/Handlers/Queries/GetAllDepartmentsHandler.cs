using Domain.Entities.Commons.Generic;
using Domain.Interfaces;
using MediatR;

namespace Application.Generic.Handlers.Queries
{
    public class GetAllDepartmentsQuery : IRequest<List<Department>>
    {
    }
    public class GetAllDepartmentsHandler : IRequestHandler<GetAllDepartmentsQuery, List<Department>>
    {
        private readonly IGenericRepository _genericRepository;

        public GetAllDepartmentsHandler(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<List<Department>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
        {
            return await _genericRepository.GetAllDepartmentsAsync();
        }
    }
}
