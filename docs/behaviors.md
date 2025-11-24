# Behaviors / Interceptors

Los **Behaviors** (o Interceptors) en MediatR permiten ejecutar lógica antes o después de los handlers de comandos/queries. Son útiles para aspectos transversales como logging, validación, performance, etc.

## Ejemplo: LoggingBehavior

El `LoggingBehavior` implementa un log antes y después de cada comando:

```csharp
public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _logger.LogInformation("INTERCEPTOR -> Executing command {CommandName}", request.GetType().Name);
        var response = await next();
        _logger.LogInformation("INTERCEPTOR -> Completed command {CommandName}", request.GetType().Name);
        return response;
    }
}
```

## Diferencias clave con Domain Events

| Característica         | Behaviors/Interceptors                | Domain Events                        |
|-----------------------|----------------------------------------|--------------------------------------|
| Propósito             | Aspectos transversales (cross-cutting) | Reaccionar a hechos del dominio      |
| Acoplamiento          | General, no específico de dominio      | Específico del dominio               |
| Momento de ejecución  | Antes/después del handler              | Tras commit de la transacción        |
| Ejemplo típico        | Logging, validación, performance       | Notificaciones, integración externa  |

## Antipatrón: lógica de negocio en Behaviors

Colocar lógica de negocio (como enviar emails, logs de negocio, etc.) en Behaviors o directamente en el handler **rompe el principio de separación de responsabilidades** y dificulta el mantenimiento.

### Código relevante
- `Application/Behaviors/LoggingBehavior.cs`
- `Application/Commands/CreateOrderWithBehavior.cs`
