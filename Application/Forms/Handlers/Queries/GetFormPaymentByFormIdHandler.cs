using Domain.Entities.Forms;
using Domain.Interfaces;
using MediatR;

namespace Application.Forms.Handlers.Queries
{
    public class GetFormPaymentByFormIdQuery : IRequest<FormPayment>
    {
        public int FormId { get; set; }

        public GetFormPaymentByFormIdQuery(int formId)
        {
            FormId = formId;
        }
    }
    public class GetFormPaymentByFormIdHandler : IRequestHandler<GetFormPaymentByFormIdQuery, FormPayment>
    {
        private readonly IFormRepository _formRepository;

        public GetFormPaymentByFormIdHandler(IFormRepository formRepository)
        {
            _formRepository = formRepository;
        }

        public async Task<FormPayment> Handle(GetFormPaymentByFormIdQuery request, CancellationToken cancellationToken)
        {
            return await _formRepository.GetFormPaymentInfoByFormIdAsync(request.FormId);
        }
    }
}
