﻿namespace Application.DTOs
{
    public class FormCoverDto
    {
        public int Id { get; set; }
        public string SerialNumber { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int PaymentTotal { get; set; }
        public int isTaxed { get; set; }
        public int isReceipt { get; set; }
        public string Stage { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
