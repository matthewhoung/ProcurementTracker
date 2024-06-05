using MediatR;

namespace Application.Forms.Commands
{
    public class CreateFormDetailCommand : IRequest<int>
    {
        public int FormId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
        public int UnitId { get; set; }
    }
}
