using Domain.Entities.Commons.Generic;
using Domain.Interfaces;
using MediatR;

namespace Application.Generic.Handlers.Commands
{
    public class CreatePayTypeCommand : IRequest<int>
    {
        public PayType PayType { get; set; }
    }
    public class CreatePayTypeHandler : IRequestHandler<CreatePayTypeCommand, int>
    {
        private readonly IGenericRepository _genericRepository;

        public CreatePayTypeHandler(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<int> Handle(CreatePayTypeCommand request, CancellationToken cancellationToken)
        {
            await _genericRepository.CreatePayTypeAsync(request.PayType);
            return request.PayType.PayTypeId;
        }
    }
}
