# Domain Events (Eventos de Dominio)

Los **Domain Events** son objetos que representan hechos significativos ocurridos dentro del dominio de negocio. Permiten desacoplar la lógica principal de la aplicación de los efectos colaterales (side effects), como notificaciones, logs, envíos de emails, etc.

## ¿Qué son y por qué usarlos?

- Son mensajes inmutables que indican que "algo importante" ha sucedido en el dominio.
- Permiten que otras partes del sistema reaccionen a estos hechos sin acoplarse al flujo principal.
- Fomentan el **desacoplamiento** y la **extensibilidad**.
- Facilitan la integración con otros sistemas o bounded contexts.

## ¿Cuándo se publican?

- Se agregan al agregado raíz (por ejemplo, `Order`) durante la ejecución de la lógica de dominio.
- Se publican **después** de que la transacción principal (commit) ha sido exitosa.
- En este proyecto, la publicación se realiza en el `DomainEventsBehavior` tras el `SaveChanges()`.

## Rol de `IDomainEvent` y `DomainEventsBehavior`

| Elemento                | Rol                                                                 |
|-------------------------|---------------------------------------------------------------------|
| `IDomainEvent`          | Contrato base para todos los eventos de dominio.                     |
| `DomainEventsBehavior`  | Interceptor que detecta y publica los eventos tras el commit.        |

### Ejemplo de flujo

```csharp
// En el agregado Order
order.AddDomainEvent(new OrderCreatedEvent(...));

// Tras SaveChanges(), el DomainEventsBehavior publica el evento:
await _publisher.Publish(domainEvent);
```

### Ventajas
- Desacopla la lógica de negocio de los efectos colaterales.
- Permite añadir nuevos handlers sin modificar el dominio.
- Facilita pruebas y mantenimiento.

### Código relevante
- `Domain/Events/IDomainEvent.cs`
- `Domain/Entities/AggregateRoot.cs`
- `Application/Behaviors/DomainEventsBehavior.cs`
- `Application/DomainEvents/OrderCreatedHandler.cs`
