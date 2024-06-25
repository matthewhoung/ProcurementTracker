using Domain.Entities.Forms;

namespace Application.Services
{
    public class PaymentCalculationService
    {
        public void CalculatePayment(FormPayment formPayment)
        {
            var paymentAmount = formPayment.PaymentDelta > 0 ?
                    formPayment.PaymentAmount - formPayment.PaymentDelta :
                    formPayment.PaymentAmount;

            var taxAmount = 0;
            var paymentTotal = paymentAmount;

            switch ((formPayment.IsTaxed, formPayment.IsReceipt))
            {
                case (1, 1):
                    taxAmount = Math.Min((paymentAmount - (int)(paymentAmount / 1.05)), (int)(paymentAmount * 0.05));
                    paymentTotal = paymentAmount;
                    break;
                case (0, 1):
                    paymentTotal = (int)(paymentAmount * 1.05);
                    taxAmount = paymentTotal - paymentAmount;
                    break;
                case (0, 0):
                    paymentTotal = paymentAmount;
                    break;
                default:
                    throw new ArgumentException("Invalid tax and receipt values");
            }

            formPayment.PaymentAmount = paymentAmount;
            formPayment.TaxAmount = taxAmount;
            formPayment.PaymentTotal = paymentTotal;
        }
    }
}
