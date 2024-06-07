using Domain.Entities.Forms;
using Domain.Interfaces;
using MediatR;

namespace Application.Forms.Handlers.Queries
{
    public class GetUnSignedMemberQuery : IRequest<FormSignatureMember>
    {
        public int FormId { get; set; }

        public GetUnSignedMemberQuery(int formId)
        {
            FormId = formId;
        }
    }
    public class GetUnSignedMemberHandler : IRequestHandler<GetUnSignedMemberQuery, FormSignatureMember>
    {
        private readonly IFormRepository _formRepository;

        public GetUnSignedMemberHandler(IFormRepository formRepository)
        {
            _formRepository = formRepository;
        }

        public async Task<FormSignatureMember> Handle(GetUnSignedMemberQuery request, CancellationToken cancellationToken)
        {
            var formSignatureMember = await _formRepository.GetUnSignedMemberAsync(request.FormId);
            return formSignatureMember;
        }
    }
}
