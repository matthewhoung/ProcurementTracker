using Domain.Entities.Commons.Generic;
using Domain.Interfaces;
using MediatR;

namespace Application.Generic.Handlers.Queries
{
    public class GetAllPayByQuery : IRequest<List<PayBy>>
    {
    }
    public class GetAllPayByHandler : IRequestHandler<GetAllPayByQuery, List<PayBy>>
    {
        private readonly IGenericRepository _genericRepository;

        public GetAllPayByHandler(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<List<PayBy>> Handle(GetAllPayByQuery request, CancellationToken cancellationToken)
        {
            return await _genericRepository.GetAllPayBysAsync();
        }
    }
}
