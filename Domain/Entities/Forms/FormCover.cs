namespace Domain.Entities.Forms
{
    public class FormCover
    {
        public int Id { get; set; }
        public string SerialNumber { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int PaymentTotal { get; set; }
        public int IsDelta { get; set; }
        public int IsTaxed { get; set; }
        public int IsReceipt { get; set; }
        public string Stage { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
