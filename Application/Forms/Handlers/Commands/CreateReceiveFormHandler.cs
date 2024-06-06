using Domain.Interfaces;
using MediatR;

namespace Application.Forms.Handlers.Commands
{
    public class CreateReceiveFormCommand : IRequest<int>
    {
        public int FormId { get; set; }
    }

    public class CreateReceiveFormCommandHandler : IRequestHandler<CreateReceiveFormCommand, int>
    {
        private readonly IFormRepository _formRepository;

        public CreateReceiveFormCommandHandler(IFormRepository formRepository)
        {
            _formRepository = formRepository;
        }

        public async Task<int> Handle(CreateReceiveFormCommand request, CancellationToken cancellationToken)
        {
            return await _formRepository.CreateReceiveFormAsync(request.FormId);
        }
    }

}
