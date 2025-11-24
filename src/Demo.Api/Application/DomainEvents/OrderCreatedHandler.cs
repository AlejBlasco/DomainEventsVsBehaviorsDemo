using Demo.Api.Domain.Events;
using MediatR;

namespace Demo.Api.Application.DomainEvents;

public class OrderCreatedHandler
    : INotificationHandler<OrderCreatedEvent>
{
    private readonly ILogger<OrderCreatedHandler> _logger;

    public OrderCreatedHandler(ILogger<OrderCreatedHandler> logger) => _logger = logger;

    public Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        // Put here your logic to handle the event, e.g., send an email, update a read model, etc.
        _logger.LogWarning("DOMAIN EVENT → New order for {Client}. Total: {Total:C}",
            notification.CustomerName, notification.Total);

        return Task.CompletedTask;
    }
}
