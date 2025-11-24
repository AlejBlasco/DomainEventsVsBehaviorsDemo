using Demo.Api.Application.Behaviors;
using Demo.Api.Application.Commands;
using Demo.Api.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("DemoDb"));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

// Behaviors / Interceptors
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(DomainEventsBehavior<,>));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// Endpoints
app.MapPost("/with-domain-events", async (CreateOrderWithDomainEventCommand cmd, ISender sender) =>
{
    var id = await sender.Send(cmd);
    return Results.Created($"/orders/{id}", new { id });
}).WithName("WithDomainEvents");

app.MapPost("/with-behavior", async (CreateOrderWithBehaviorCommand cmd, ISender sender) =>
{
    var id = await sender.Send(cmd);
    return Results.Created($"/orders/{id}", new { id });
}).WithName("WithBehavior");

await app.RunAsync();
