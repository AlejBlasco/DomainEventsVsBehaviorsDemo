# Cómo ejecutar el proyecto paso a paso

1. **Clona el repositorio:**
   ```bash
   git clone https://github.com/AlejBlasco/DomainEventsVsBehaviorsDemo.git
   cd DomainEventsVsBehaviorsDemo
   ```

2. **Ejecuta la API:**
   ```bash
   dotnet run --project src/Demo.Api
   ```

3. **Abre Swagger:**
   - Al iniciar, la consola mostrará la URL de Swagger (por defecto: http://localhost:5000/swagger o similar).
   - Swagger UI se abre automáticamente y permite probar los endpoints.

4. **Probar los endpoints POST:**

   - **Con Domain Events:**
     - Endpoint: `POST /with-domain-events`
     - Body de ejemplo:
       ```json
       {
         "customerName": "Juan Pérez",
         "total": 123.45
       }
       ```

   - **Con Behavior (antipatrón):**
     - Endpoint: `POST /with-behavior`
     - Body de ejemplo:
       ```json
       {
         "customerName": "Ana García",
         "total": 99.99
       }
       ```

5. **Verifica la respuesta:**
   - Ambos endpoints retornan el `id` del pedido creado.
