using Demo.Api.Domain.Entities;
using Demo.Api.Infrastructure.Persistence;
using MediatR;

namespace Demo.Api.Application.Commands;

public record CreateOrderWithDomainEventCommand(string CustomerName, decimal Total) : IRequest<Guid>;

public class CreateOrderWithDomainEventHandler : IRequestHandler<CreateOrderWithDomainEventCommand, Guid>
{
    private readonly AppDbContext _db;

    public CreateOrderWithDomainEventHandler(AppDbContext db) => _db = db;

    public async Task<Guid> Handle(CreateOrderWithDomainEventCommand request, CancellationToken cancellationToken)
    {
        var order = Order.Create(request.CustomerName, request.Total);

        _db.Orders.Add(order);
        await _db.SaveChangesAsync(cancellationToken); // Here the domain events are dispatched

        return order.Id;
    }
}
