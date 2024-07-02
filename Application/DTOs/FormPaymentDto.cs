namespace Application.DTOs
{
    public class FormPaymentDto
    {
        public int PaymentToolId { get; set; }
        public int PaymentAmount { get; set; }
        public int PaymentDelta { get; set; }
        public int PaymentTotal { get; set; }
        public int TaxAmount { get; set; }
        public int IsDelta { get; set; }
        public int IsTaxed { get; set; }
        public int IsReceipt { get; set; }
    }

}
