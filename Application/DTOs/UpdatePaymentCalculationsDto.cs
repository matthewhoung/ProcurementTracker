﻿namespace Application.DTOs
{
    public class UpdatePaymentCalculationsDto
    {
        public int FormId { get; set; }
        public int PaymentDelta { get; set; }
        public int DeltaTitleId { get; set; }
        public int PaymentTitleId { get; set; }
        public int PaymentToolId { get; set; }
    }
}
