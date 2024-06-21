namespace Domain.Entities.Forms
{
    public class Form
    {
        public int Id { get; set; }
        public List<string> SerialNumber { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Stage { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
