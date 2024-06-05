using MediatR;

namespace Application.Forms.Commands
{
    public class CreateFormWorkerCommand : IRequest<int>
    {
        public int FormId { get; set; }
        public int WorkerTypeId { get; set; }
        public int WorkerTeamId { get; set; }
    }
}
