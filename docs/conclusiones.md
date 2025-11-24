# Conclusiones y buenas prácticas

- ? **Usa Domain Events** para efectos colaterales de negocio: son la forma recomendada en DDD y Clean Architecture.
- ? **Evita lógica de negocio en Behaviors o Handlers**: sólo para aspectos transversales (logging, validación, etc).
- ? **Desacopla** la lógica de dominio de los efectos secundarios.
- ? **Publica los Domain Events tras el commit** para garantizar consistencia.
- ? **Aprovecha Behaviors** para logging, performance, validaciones, etc.
- ? **Sigue CQRS**: separa comandos (escritura) de queries (lectura).
- ? **Mantén el dominio puro** y enfocado en las reglas de negocio.

---

> "La arquitectura limpia no es sólo una moda, es la base para sistemas mantenibles y escalables."
