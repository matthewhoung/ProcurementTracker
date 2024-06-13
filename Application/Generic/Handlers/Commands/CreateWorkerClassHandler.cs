using Domain.Entities.Commons.Generic;
using Domain.Interfaces;
using MediatR;

namespace Application.Generic.Handlers.Commands
{
    public class CreateWorkerClassCommand : IRequest<int>
    {
        public WorkerClass WorkerClass { get; set; }
    }
    public class CreateWorkerClassHandler : IRequestHandler<CreateWorkerClassCommand, int>
    {
        private readonly IGenericRepository _genericRepository;

        public CreateWorkerClassHandler(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<int> Handle(CreateWorkerClassCommand request, CancellationToken cancellationToken)
        {
            await _genericRepository.CreateWorkerClassAsync(request.WorkerClass);
            return request.WorkerClass.WorkerClassId;
        }
    }
}
