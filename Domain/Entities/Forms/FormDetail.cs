namespace Domain.Entities.Forms
{
    public class FormDetail
    {
        public int DetailId { get; set; }
        public int FormId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
        public int UnitId { get; set; }
        public int Total { get; set; }
        public bool IsChecked { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
