using Demo.Api.Domain.Entities;
using Demo.Api.Infrastructure.Persistence;
using MediatR;

namespace Demo.Api.Application.Behaviors;

public class DomainEventsBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
where TRequest : IRequest<TResponse>
{
    private readonly AppDbContext _db;
    private readonly IPublisher _publisher;

    public DomainEventsBehavior(AppDbContext db, IPublisher publisher)
    {
        _db = db;
        _publisher = publisher;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var response = await next();

        // Only after SaveChanges (commit) is successful, publish domain events
        var aggregates = _db.ChangeTracker
            .Entries<AggregateRoot>()
            .Where(e => e.Entity.DomainEvents.Any())
            .Select(e => e.Entity);

        var events = aggregates
            .SelectMany(x => x.DomainEvents)
            .ToList();

        foreach (var aggregate in aggregates)
            aggregate.ClearDomainEvents();

        foreach (var domainEvent in events)
            await _publisher.Publish(domainEvent, cancellationToken);

        return response;
    }
}
