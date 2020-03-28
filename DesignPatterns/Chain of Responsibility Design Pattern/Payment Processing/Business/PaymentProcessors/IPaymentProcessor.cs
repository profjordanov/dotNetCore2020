using Payment_processing.Business.Models;

namespace Payment_processing.Business.PaymentProcessors
{
    public interface IPaymentProcessor
    {
        void Finalize(Order order);
    }
}
