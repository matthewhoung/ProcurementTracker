using Domain.Interfaces;
using MediatR;

namespace Application.Forms.Handlers.Commands
{
    public class CreateAffiliateFormCommand : IRequest<int>
    {
        public int FormId { get; set; }
        public int AffiliateFormId { get; set; }
    }
    public class CreateAffiliateFormHandler : IRequestHandler<CreateAffiliateFormCommand, int>
    {
        private readonly IFormRepository _formRepository;

        public CreateAffiliateFormHandler(IFormRepository formRepository)
        {
            _formRepository = formRepository;
        }
        public async Task<int> Handle(CreateAffiliateFormCommand request, CancellationToken cancellationToken)
        {
            var affiliateId = await _formRepository.CreateAffiliateFormAsync(request.FormId, request.AffiliateFormId);
            return affiliateId;
        }
    }
}
