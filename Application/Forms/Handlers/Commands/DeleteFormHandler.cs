using Domain.Interfaces;
using MediatR;

namespace Application.Forms.Handlers.Commands
{
    public class DeleteFormCommand : IRequest<Unit>
    {
        public int FormId { get; set; }

        public DeleteFormCommand(int formId)
        {
            FormId = formId;
        }
    }

    public class DeleteFormCommandHandler : IRequestHandler<DeleteFormCommand, Unit>
    {
        private readonly IFormRepository _formRepository;

        public DeleteFormCommandHandler(IFormRepository formRepository)
        {
            _formRepository = formRepository;
        }

        public async Task<Unit> Handle(DeleteFormCommand request, CancellationToken cancellationToken)
        {
            await _formRepository.DeleteFormAsync(request.FormId);
            return Unit.Value;
        }
    }

}
