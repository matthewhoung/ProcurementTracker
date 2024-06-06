using Domain.Interfaces;
using MediatR;

namespace Application.Forms.Handlers.Commands
{
    public class CreateOrderFormCommand : IRequest<int>
    {
        public int FormId { get; set; }
    }

    public class CreateOrderFormCommandHandler : IRequestHandler<CreateOrderFormCommand, int>
    {
        private readonly IFormRepository _formRepository;

        public CreateOrderFormCommandHandler(IFormRepository formRepository)
        {
            _formRepository = formRepository;
        }

        public async Task<int> Handle(CreateOrderFormCommand request, CancellationToken cancellationToken)
        {
            return await _formRepository.CreateOrderFormAsync(request.FormId);
        }
    }

}
