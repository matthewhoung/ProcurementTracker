namespace Domain.Entities.Commons.Generic
{
    public class WorkerTeam
    {
        public int WorkerTeamId { get; set; }
        public int WorkerTypeId { get; set; }
        public string WorkerTeamName { get; set; }
        public string ContactName { get; set; }
        public string ContactNumber { get; set; }
        public string ContactAddress { get; set; }
        public int AccountCode { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
    }
}
