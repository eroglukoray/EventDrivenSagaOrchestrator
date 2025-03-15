using MassTransit;
using Shared.Events;

namespace StockService.Consumers
{
    public class PaymentSucceededConsumer : IConsumer<PaymentSucceeded>
    {
        public async Task Consume(ConsumeContext<PaymentSucceeded> context)
        {
            Console.WriteLine($"[Stock Service] Payment Successful. Updating Stock for Order {context.Message.OrderId}");

            var isStockAvailable = new Random().Next(0, 2) == 1;

            if (isStockAvailable)
            {
                await context.Publish(new StockReserved(context.Message.OrderId));
                Console.WriteLine($"[Stock Service] Stock Reserved for Order {context.Message.OrderId}");
            }
            else
            {
                await context.Publish(new StockFailed(context.Message.OrderId));
                Console.WriteLine($"[Stock Service] Stock Reservation FAILED for Order {context.Message.OrderId}");
            }
        }
    }
}
