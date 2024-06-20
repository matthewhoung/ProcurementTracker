namespace Domain.Entities.Commons.Generic
{
    public class WorkerType
    {
        public int WorkerTypeId { get; set; }
        public int WorkerClassId { get; set; }
        public string WorkerTypeName { get; set; }
        public int WorkerTypeSort { get; set; }
        public string WorkerTypeIcon { get; set; }
    }
}
