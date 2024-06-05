using Application.Forms.Queries;
using Domain.Entities.Forms;
using Domain.Interfaces;
using MediatR;

namespace Application.Forms.Handlers
{
    public class GetFormByIdHandler : IRequestHandler<GetFormByIdQuery, Form>
    {
        private readonly IFormRepository _formRepository;

        public GetFormByIdHandler(IFormRepository formRepository)
        {
            _formRepository = formRepository;
        }
        public async Task<Form> Handle(GetFormByIdQuery request, CancellationToken cancellationToken)
        {
            var form = await _formRepository.GetFormByIdAsync(request.Id);
            return form;
        }
    }
}
