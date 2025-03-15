using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Shared.Events;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        private readonly IBus _bus;

        public OrderController(IBus bus)
        {
            _bus = bus;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(string productName, int quantity)
        {
            var orderId = Guid.NewGuid();
            var message = new OrderCreated(orderId, productName, quantity);

            await _bus.Publish(message);

            return Ok($"Order {orderId} created and published.");
        }
    }
}
