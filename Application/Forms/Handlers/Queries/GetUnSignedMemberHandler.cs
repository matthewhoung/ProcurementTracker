using Domain.Entities.Forms;
using Domain.Interfaces;
using MediatR;

namespace Application.Forms.Handlers.Queries
{
    public class GetUnSignedMembersQuery : IRequest<List<FormSignatureMember>>
    {
        public int FormId { get; set; }

        public GetUnSignedMembersQuery(int formId)
        {
            FormId = formId;
        }
    }

    public class GetUnSignedMembersHandler : IRequestHandler<GetUnSignedMembersQuery, List<FormSignatureMember>>
    {
        private readonly IFormRepository _formRepository;

        public GetUnSignedMembersHandler(IFormRepository formRepository)
        {
            _formRepository = formRepository;
        }

        public async Task<List<FormSignatureMember>> Handle(GetUnSignedMembersQuery request, CancellationToken cancellationToken)
        {
            var formSignatureMembers = await _formRepository.GetUnSignedMembersAsync(request.FormId);
            return formSignatureMembers;
        }
    }
}
