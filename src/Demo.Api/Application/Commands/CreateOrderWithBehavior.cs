using Demo.Api.Domain.Entities;
using Demo.Api.Infrastructure.Persistence;
using MediatR;

namespace Demo.Api.Application.Commands;

public record CreateOrderWithBehaviorCommand(string CustomerName, decimal Total) : IRequest<Guid>;

public class CreateOrderWithBehaviorHandler : IRequestHandler<CreateOrderWithBehaviorCommand, Guid>
{
    private readonly AppDbContext _db;
    private readonly ILogger<CreateOrderWithBehaviorHandler> _logger;

    public CreateOrderWithBehaviorHandler(AppDbContext db, ILogger<CreateOrderWithBehaviorHandler> logger)
    {
        _db = db;
        _logger = logger;
    } 

    public async Task<Guid> Handle(CreateOrderWithBehaviorCommand request, CancellationToken cancellationToken)
    {
        var order = Order.Create(request.CustomerName, request.Total);

        _db.Orders.Add(order);
        await _db.SaveChangesAsync(cancellationToken);

        // Logging inside the handler (DDD Anti-pattern)
        _logger.LogError("BEHAVIOR: Order created for Client: {Client} (logic inside handler)", order.CustomerName);

        return order.Id;
    }
}
