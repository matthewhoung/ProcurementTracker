namespace Domain.Entities.Commons.Generic
{
    public class Workers
    {
        public int WorkerClassId { get; set; }
        public string WorkerClassName { get; set; }
        public List<WorkerType> WorkerTypes { get; set; }
    }
}
