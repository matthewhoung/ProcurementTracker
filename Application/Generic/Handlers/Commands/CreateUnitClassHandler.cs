using Domain.Entities.Commons.Generic;
using Domain.Interfaces;
using MediatR;

namespace Application.Generic.Handlers.Commands
{
    public class CreateUnitClassCommand : IRequest<int>
    {
        public UnitClass UnitClass { get; set; }
    }
    public class CreateUnitClassHandler : IRequestHandler<CreateUnitClassCommand, int>
    {
        private readonly IGenericRepository _genericRepository;

        public CreateUnitClassHandler(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<int> Handle(CreateUnitClassCommand request, CancellationToken cancellationToken)
        {
            await _genericRepository.CreateUnitAsync(request.UnitClass);
            return request.UnitClass.UnitId;
        }
    }
}
