
namespace Shared.Events
{
    public record OrderCreated(Guid OrderId, string ProductName, int Quantity);
}