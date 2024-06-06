﻿using Domain.Entities.Commons.FileUploader;
using Domain.Entities.Forms;

namespace Application.DTOs
{
    public class FormInfoDto
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Stage { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<FormDetail> Details { get; set; }
        public List<FormSignatureMember> Signatures { get; set; }
        public List<FormWorker> Workers { get; set; }
        public List<FormPayment> Payments { get; set; }
        public List<FormDepartment> Departments { get; set; }
        public List<FileUploader> FilePaths { get; set; }
    }
}