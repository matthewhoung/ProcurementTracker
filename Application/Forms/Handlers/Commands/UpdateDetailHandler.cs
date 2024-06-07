using Domain.Entities.Forms;
using Domain.Interfaces;
using MediatR;

namespace Application.Forms.Handlers.Commands
{
    public class UpdateDetailCommand : IRequest<Unit>
    {
        public FormDetail FormDetail { get; set; }
    }

    public class UpdateDetailCommandHandler : IRequestHandler<UpdateDetailCommand, Unit>
    {
        private readonly IFormRepository _formRepository;

        public UpdateDetailCommandHandler(IFormRepository formRepository)
        {
            _formRepository = formRepository;
        }

        public async Task<Unit> Handle(UpdateDetailCommand request, CancellationToken cancellationToken)
        {
            await _formRepository.UpdateDetailAsync(request.FormDetail);
            return Unit.Value;
        }
    }
}
