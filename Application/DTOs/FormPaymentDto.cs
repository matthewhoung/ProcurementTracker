namespace Application.DTOs
{
    public class FormPaymentDto
    {
        public int PaymentTitleId { get; set; }
        public int PaymentToolId { get; set; }
        public int PaymentDelta { get; set; }
        public int IsTaxed { get; set; }
        public int IsReceipt { get; set; }
    }

}
