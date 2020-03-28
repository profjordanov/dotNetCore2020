using Payment_processing.Business.Models;
using System.Linq;

namespace Payment_processing.Business.PaymentProcessors
{
    public class CreditCardPaymentProcessor : IPaymentProcessor
    {
        public void Finalize(Order order)
        {
            // Invoke the Stripe API
            var payment = order.SelectedPayments
                .FirstOrDefault(x => x.PaymentProvider == PaymentProvider.CreditCard);

            if (payment == null) return;

            order.FinalizedPayments.Add(payment);
        }
    }
}
