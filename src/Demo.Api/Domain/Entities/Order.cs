using Demo.Api.Domain.Events;

namespace Demo.Api.Domain.Entities;

public class Order : AggregateRoot
{
    public Guid Id { get; private set; }
    public string CustomerName { get; private set; } = null!;
    public decimal Total { get; private set; }
    public DateTime CreationDate { get; private set; }

   
    public static Order Create(string customerName, decimal total)
    {
        var order = new Order
        {
            Id = Guid.NewGuid(),
            CustomerName = customerName,
            Total = total,
            CreationDate = DateTime.UtcNow
        };

        order.AddDomainEvent(new OrderCreatedEvent(order.Id, 
            order.CustomerName, 
            order.Total));

        return order;
    }

    
}
