using Domain.Entities.Forms;
using MediatR;

namespace Application.Forms.Queries
{
    public class GetFormSignatureMemberByFormIdQuery : IRequest<List<FormSignatureMember>>
    {
        public int FormId { get; set; }

        public GetFormSignatureMemberByFormIdQuery(int formId)
        {
            FormId = formId;
        }
    }
}
