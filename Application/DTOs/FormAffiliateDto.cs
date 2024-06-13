namespace Application.DTOs
{
    public class FormAffiliateDto
    {
        public int FormId { get; set; }
        public int AffiliateFormId { get; set; }
        public FormInfoDto AffiliateFormInfo { get; set; }
    }
}
