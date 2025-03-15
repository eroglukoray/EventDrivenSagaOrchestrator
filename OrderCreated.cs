
namespace OrderService.Models
{
    public record OrderCreated(Guid OrderId, string ProductName, int Quantity);
}
