namespace Domain.Entities.Commons.Generic
{
    public class Project
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public int Level { get; set; }
        public int Sort { get; set; }
        public int Status { get; set; }
        public string? Color { get; set; }
    }
}
