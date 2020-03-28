using Payment_processing.Business.Models;
using System.Linq;

namespace Payment_processing.Business.PaymentProcessors
{
    public class InvoicePaymentProcessor : IPaymentProcessor
    {
        public void Finalize(Order order)
        {
            // Create an invoice and email it

            var payment = order.SelectedPayments
                .FirstOrDefault(x => x.PaymentProvider == PaymentProvider.Invoice);

            if (payment == null) return;

            order.FinalizedPayments.Add(payment);
        }
    }
}
