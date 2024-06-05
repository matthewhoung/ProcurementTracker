using Application.Forms.Commands;
using Domain.Entities.Forms;
using Domain.Interfaces;
using MediatR;

namespace Application.Forms.Handlers
{
    public class CreateFormHandler : IRequestHandler<CreateFormCommand, int>
    {
        private readonly IFormRepository _formRepository;

        public CreateFormHandler(IFormRepository formRepository)
        {
            _formRepository = formRepository;
        }

        public async Task<int> Handle(CreateFormCommand request, CancellationToken cancellationToken)
        {
            var form = new Form
            {
                ProjectId = request.ProjectId,
                Title = request.Title,
                Description = request.Description,
                Stage = "採購單",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var formId = await _formRepository.CreateFormAsync(form);
            return formId;
        }
    }
}
