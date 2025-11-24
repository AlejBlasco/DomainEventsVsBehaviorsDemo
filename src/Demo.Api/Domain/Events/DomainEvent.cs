namespace Demo.Api.Domain.Events;

public abstract record DomainEvent : IDomainEvent
{
    //public DateTime OccurredOnUtc { get; } = DateTime.UtcNow;
    //public Guid EventId { get; } = Guid.NewGuid();
}
