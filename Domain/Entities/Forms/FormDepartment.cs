namespace Domain.Entities.Forms
{
    public class FormDepartment
    {
        public int FormDepartmentId { get; set; }
        public int FormId { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }
}
