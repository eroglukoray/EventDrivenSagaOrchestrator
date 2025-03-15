using Shared.Events;
using MassTransit;

namespace Shared.Consumers
{
    public class OrderCreatedConsumer : IConsumer<OrderCreated>
    {
        public async Task Consume(ConsumeContext<OrderCreated> context)
        {
            Console.WriteLine($"[Payment Service] Order Received: {context.Message.OrderId}");

            var isSuccess = new Random().Next(0, 2) == 1;

            if (isSuccess)
            {
                Console.WriteLine($"[Payment Service] Payment SUCCESS for Order {context.Message.OrderId}");
                await context.Publish(new PaymentSucceeded(context.Message.OrderId));
            }
            else
            {
                Console.WriteLine($"[Payment Service] Payment FAILED for Order {context.Message.OrderId}");
                await context.Publish(new PaymentFailed(context.Message.OrderId));
            }
        }
    }
}
