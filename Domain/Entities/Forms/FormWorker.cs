namespace Domain.Entities.Forms
{
    public class FormWorker
    {
        public int WorkerId { get; set; }
        public int FormId { get; set; }
        public int WorkerTypeId { get; set; }
        public string WorkerTypeName { get; set; }
        public int WorkerTeamId { get; set; }
        public string WorkerTeamName { get; set; }
    }
}
