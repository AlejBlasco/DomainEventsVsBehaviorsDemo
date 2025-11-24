using MediatR;

namespace Demo.Api.Domain.Events;

public sealed record OrderCreatedEvent(Guid OrderId, string CustomerName, decimal Total) : IDomainEvent, INotification
{
}
