# Documentación OpenAPI (Swagger)

La API expone dos endpoints principales para comparar ambos enfoques:

## Endpoints disponibles

### 1. Crear pedido usando Domain Events (enfoque correcto)

- **POST** `/with-domain-events`
- **Descripción:** Crea un pedido y dispara un Domain Event tras el commit.
- **Request Body:**

```json
{
  "customerName": "Juan Pérez",
  "total": 123.45
}
```

- **Response 201:**

```json
{
  "id": "b1e2f3a4-..."
}
```

### 2. Crear pedido con lógica directa (antipatrón)

- **POST** `/with-behavior`
- **Descripción:** Crea un pedido y ejecuta la lógica de log directamente en el handler.
- **Request Body:**

```json
{
  "customerName": "Ana García",
  "total": 99.99
}
```

- **Response 201:**

```json
{
  "id": "c2f3e4b5-..."
}
```

## Esquema de los modelos

### CreateOrderWithDomainEventCommand / CreateOrderWithBehaviorCommand

| Campo         | Tipo    | Obligatorio | Descripción                |
|-------------- |---------|-------------|----------------------------|
| customerName  | string  | Sí          | Nombre del cliente         |
| total         | decimal | Sí          | Importe total del pedido   |

### Respuesta

| Campo | Tipo   | Descripción                |
|-------|--------|----------------------------|
| id    | guid   | Identificador del pedido   |

## Uso de Swagger UI

- Al ejecutar el proyecto (`dotnet run`), Swagger UI se abre automáticamente en `/swagger`.
- Desde ahí puedes probar ambos endpoints, ver los modelos y las respuestas.

> ? **Tip:** Puedes copiar y pegar los ejemplos de JSON directamente en Swagger UI para probar la API.
