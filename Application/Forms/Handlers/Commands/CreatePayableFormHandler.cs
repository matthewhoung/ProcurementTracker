using Domain.Interfaces;
using MediatR;

namespace Application.Forms.Handlers.Commands
{
    public class CreatePayableFormCommand : IRequest<int>
    {
        public int FormId { get; set; }
    }

    public class CreatePayableFormCommandHandler : IRequestHandler<CreatePayableFormCommand, int>
    {
        private readonly IFormRepository _formRepository;

        public CreatePayableFormCommandHandler(IFormRepository formRepository)
        {
            _formRepository = formRepository;
        }

        public async Task<int> Handle(CreatePayableFormCommand request, CancellationToken cancellationToken)
        {
            return await _formRepository.CreatePayableFormAsync(request.FormId);
        }
    }

}
