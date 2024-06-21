namespace Application.DTOs
{
    public class FormFilterDto
    {
        public int? FormId { get; set; }
        public int? UserId { get; set; }
        public string? Stage { get; set; }
        public string? Status { get; set; }
    }
}
