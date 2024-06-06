using Domain.Entities.Forms;
using Domain.Interfaces;
using MediatR;

namespace Application.Forms.Handlers.Queries
{
    public class GetFormSignatureMemberByFormIdQuery : IRequest<List<FormSignatureMember>>
    {
        public int FormId { get; set; }

        public GetFormSignatureMemberByFormIdQuery(int formId)
        {
            FormId = formId;
        }
    }
    public class GetFormSignatureMemberByFormIdHandler : IRequestHandler<GetFormSignatureMemberByFormIdQuery, List<FormSignatureMember>>
    {
        private readonly IFormRepository _formRepository;

        public GetFormSignatureMemberByFormIdHandler(IFormRepository formRepository)
        {
            _formRepository = formRepository;
        }

        public async Task<List<FormSignatureMember>> Handle(GetFormSignatureMemberByFormIdQuery request, CancellationToken cancellationToken)
        {
            var formSignatureMembers = await _formRepository.GetFormSignatureMembersByFormIdAsync(request.FormId);
            return formSignatureMembers;
        }
    }
}
