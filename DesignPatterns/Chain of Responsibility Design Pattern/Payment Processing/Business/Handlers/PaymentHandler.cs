using Payment_processing.Business.Exceptions;
using Payment_processing.Business.Models;
using System;
using System.Collections.Generic;

namespace Payment_processing.Business.Handlers.PaymentHandlers
{
    public class PaymentHandler
    {
        private readonly IList<IReceiver<Order>> receivers;

        public PaymentHandler(params IReceiver<Order>[] receivers)
        {
            this.receivers = receivers;
        }

        public void Handle(Order order)
        {
            foreach (var receiver in receivers)
            {
                Console.WriteLine($"Running: {receiver.GetType().Name}");

                if (order.AmountDue > 0)
                {
                    receiver.Handle(order);
                }
                else
                {
                    break;
                }
            }

            if (order.AmountDue > 0)
            {
                throw new InsufficientPaymentException();
            }
            else
            {
                order.ShippingStatus = ShippingStatus.ReadyForShippment;
            }
        }

        public void SetNext(IReceiver<Order> next)
        {
            receivers.Add(next);
        }
    }
}
