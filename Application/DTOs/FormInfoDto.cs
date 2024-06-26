using Domain.Entities.Commons.FileUploader;
using Domain.Entities.Forms;

namespace Application.DTOs
{
    public class FormInfoDto
    {
        public int Id { get; set; }
        public string SerialNumber { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int PaymentTotal { get; set; }
        public string Stage { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<FormDetail> Details { get; set; }
        public List<FormSignatureMember> Signatures { get; set; }
        public FormWorker Workers { get; set; }
        public FormPayment Payments { get; set; }
        public FormDepartment Departments { get; set; }
        public List<FileUploader> FilePaths { get; set; }
        public List<FormAffiliateDto> Affiliates { get; set; }
    }
}
