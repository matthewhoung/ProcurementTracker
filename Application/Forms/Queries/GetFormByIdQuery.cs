using Application.DTOs;
using Domain.Entities.Forms;
using MediatR;

namespace Application.Forms.Queries
{
    public class GetFormByIdQuery : IRequest<FormInfoDto>
    {
        public int FormId { get; set; }

        public GetFormByIdQuery(int formId)
        {
            FormId = formId;
        }
    }
}
