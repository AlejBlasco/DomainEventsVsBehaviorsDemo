# Qué observar en la consola (logs)

Al ejecutar y probar ambos endpoints, observa los logs generados:

## 1. Con Domain Events (enfoque correcto)

- Se muestra un log del interceptor (LoggingBehavior):
  ```
  info: INTERCEPTOR -> Executing command CreateOrderWithDomainEventCommand
  info: INTERCEPTOR -> Completed command CreateOrderWithDomainEventCommand
  warning: DOMAIN EVENT ? New order for Juan Pérez. Total: $123.45
  ```
- El log de negocio ("DOMAIN EVENT") aparece **después** del commit.

## 2. Con Behavior (antipatrón)

- Se muestra un log del interceptor (LoggingBehavior):
  ```
  info: INTERCEPTOR -> Executing command CreateOrderWithBehaviorCommand
  info: INTERCEPTOR -> Completed command CreateOrderWithBehaviorCommand
  error: BEHAVIOR: Order created for Client: Ana García (logic inside handler)
  ```
- El log de negocio aparece **dentro** del handler, no tras el commit.

## Diferencias clave

- El enfoque con Domain Events desacopla el log de negocio y lo ejecuta tras el commit.
- El antipatrón mezcla lógica de negocio y persistencia, dificultando la escalabilidad y el mantenimiento.
