using Domain.Entities.Forms;
using MediatR;

namespace Application.Forms.Queries
{
    public class GetAllFormsQuery : IRequest<List<Form>>
    {
    }
}
