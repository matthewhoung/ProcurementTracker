namespace Domain.Entities.Forms
{
    public class FormPayment
    {
        public int PaymentId { get; set; }
        public int FormId { get; set; }
        public int PaymentTotal { get; set; }
        public int PaymentDelta { get; set; }
        public int DeltaTitleId { get; set; }
        public string DeltaTitle { get; set; }
        public int PaymentAmount { get; set; }
        public int PaymentTitleId { get; set; }
        public string PaymentTitle { get; set; }
        public int PaymentToolId { get; set; }
        public string PaymentTool { get; set; }
    }
}
