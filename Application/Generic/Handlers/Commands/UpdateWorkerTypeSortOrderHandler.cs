using Domain.Entities.Commons.Generic;
using Domain.Interfaces;
using MediatR;

namespace Application.Generic.Handlers.Commands
{
    public class UpdateWorkerTypeSortOrderCommand : IRequest<Unit>
    {
        public List<WorkerTypeSort> WorkerTypeSortOrders { get; set; }
    }
    public class UpdateWorkerTypeSortOrderHandler : IRequestHandler<UpdateWorkerTypeSortOrderCommand, Unit>
    {
        private readonly IGenericRepository _genericRepository;

        public UpdateWorkerTypeSortOrderHandler(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<Unit> Handle(UpdateWorkerTypeSortOrderCommand request, CancellationToken cancellationToken)
        {
            await _genericRepository.UpdateWorkerTypeSortOrderAsync(request.WorkerTypeSortOrders);
            return Unit.Value;
        }
    }
}
