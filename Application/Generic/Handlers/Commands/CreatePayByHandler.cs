using Domain.Entities.Commons.Generic;
using Domain.Interfaces;
using MediatR;

namespace Application.Generic.Handlers.Commands
{
    public class CreatePayByCommand : IRequest<int>
    {
        public PayBy PayBy { get; set; }
    }
    public class CreatePayByHandler : IRequestHandler<CreatePayByCommand, int>
    {
        private readonly IGenericRepository _genericRepository;

        public CreatePayByHandler(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<int> Handle(CreatePayByCommand request, CancellationToken cancellationToken)
        {
            await _genericRepository.CreatePayByAsync(request.PayBy);
            return request.PayBy.PayById;
        }
    }
}
