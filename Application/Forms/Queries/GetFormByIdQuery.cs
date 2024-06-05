using Domain.Entities.Forms;
using MediatR;

namespace Application.Forms.Queries
{
    public class GetFormByIdQuery : IRequest<Form>
    {
        public int Id { get; set; }

        public GetFormByIdQuery(int id)
        {
            Id = id;
        }
    }
}
