using Domain.Entities.Commons.Generic;
using Domain.Interfaces;
using MediatR;

namespace Application.Generic.Handlers.Commands
{
    public class CreateWorkerTypeCommand : IRequest<int>
    {
        public WorkerType WorkerType { get; set; }
    }
    public class CreateWorkerTypeHandler
    {
        private readonly IGenericRepository _genericRepository;

        public CreateWorkerTypeHandler(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<int> Handle(CreateWorkerTypeCommand request, CancellationToken cancellationToken)
        {
            await _genericRepository.CreateWorkerTypeAsync(request.WorkerType);
            return request.WorkerType.WorkerTypeId;
        }
    }
}
