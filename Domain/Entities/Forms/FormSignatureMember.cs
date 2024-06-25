namespace Domain.Entities.Forms
{
    public class FormSignatureMember
    {
        public int SignId { get; set; }
        public int FormId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string Stage { get; set; }
        public bool IsChecked { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
