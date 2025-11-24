using MediatR;

namespace Demo.Api.Application.Behaviors;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger) => _logger = logger;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _logger.LogInformation("INTERCEPTOR -> Executing command {CommandName}", request.GetType().Name);

        cancellationToken.ThrowIfCancellationRequested();
        var response = await next();

        _logger.LogInformation("INTERCEPTOR -> Completed command {CommandName}", request.GetType().Name);

        return response;
    }
}
