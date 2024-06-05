using Application.Forms.Queries;
using Domain.Entities.Forms;
using Domain.Interfaces;
using MediatR;

namespace Application.Forms.Handlers
{
    public class GetAllFormsHandler : IRequestHandler<GetAllFormsQuery, List<Form>>
    {
        private readonly IFormRepository _formRepository;

        public GetAllFormsHandler(IFormRepository formRepository)
        {
            _formRepository = formRepository;
        }

        public async Task<List<Form>> Handle(GetAllFormsQuery request, CancellationToken cancellationToken)
        {
            return await _formRepository.GetAllFormsAsync();
        }
    }
}
