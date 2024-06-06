using Domain.Entities.Forms;
using Domain.Interfaces;
using MediatR;

namespace Application.Forms.Handlers.Queries
{
    public class GetFormPaymentByFormIdQuery : IRequest<List<FormPayment>>
    {
        public int FormId { get; set; }

        public GetFormPaymentByFormIdQuery(int formId)
        {
            FormId = formId;
        }
    }
    public class GetFormPaymentByFormIdHandler : IRequestHandler<GetFormPaymentByFormIdQuery, List<FormPayment>>
    {
        private readonly IFormRepository _formRepository;

        public GetFormPaymentByFormIdHandler(IFormRepository formRepository)
        {
            _formRepository = formRepository;
        }

        public async Task<List<FormPayment>> Handle(GetFormPaymentByFormIdQuery request, CancellationToken cancellationToken)
        {
            return await _formRepository.GetFormPaymentInfoByFormIdAsync(request.FormId);
        }
    }
}
