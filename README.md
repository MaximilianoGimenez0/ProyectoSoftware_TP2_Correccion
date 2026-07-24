# 🚀 Project Proposals API

> Un motor backend avanzado para la gestión, evaluación y orquestación de flujos de aprobación de propuestas de proyectos institucionales.

![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
![Entity Framework Core](https://img.shields.io/badge/EF%20Core-31A8FF?style=for-the-badge&logo=dotnet&logoColor=white)
![Clean Architecture](https://img.shields.io/badge/Clean%20Architecture-Success?style=for-the-badge)

---

# 📸 Preview

> Agrega aquí un GIF o capturas de Swagger, Postman o Insomnia mostrando el flujo completo de creación y aprobación de una propuesta.

---

# 📖 Descripción

**Project Proposals API** es una API REST desarrollada con **ASP.NET Core** para administrar el ciclo de vida de propuestas de proyectos mediante un flujo dinámico de aprobación.

El sistema determina automáticamente qué aprobadores intervienen en cada etapa según reglas de negocio, roles y el estado actual del proceso. La solución fue diseñada siguiendo principios de **Clean Architecture**, **CQRS** y **SOLID**, manteniendo una clara separación entre la lógica de negocio, la infraestructura y la capa de presentación.

---

# ✨ Características Principales

- ⚙️ Motor de flujos de aprobación basado en reglas de negocio.
- 📂 Gestión completa de propuestas de proyectos.
- 👥 Sistema de roles para aprobadores.
- 🗂️ Clasificación mediante Áreas y Tipos de Proyecto.
- 🛠️ Migraciones y Data Seeding con Entity Framework Core.
- ✅ Model Binding y validación automática mediante **ASP.NET Core Model Validation** y **Data Annotations**.
- 🧩 Arquitectura desacoplada utilizando Clean Architecture y CQRS.

---

# 🛠️ Tecnologías Utilizadas

## Backend & Core

- C#
- .NET
- ASP.NET Core Web API
- ASP.NET Core Model Binding & Validation
- Data Annotations

## Persistencia

- Entity Framework Core
- Fluent API
- SQL Server
- EF Core Migrations

## Arquitectura

- Clean Architecture
- CQRS
- Repository Pattern
- Dependency Injection

---

# 🏗️ Arquitectura

```text
Cliente
   │
HTTP REST
   │
Controllers
   │
Commands / Queries
   │
Application
   │
Domain
   │
Repositories
   │
Infrastructure
   │
SQL Server
```

---

# 🧠 Decisiones Técnicas

1. Separación de lectura y escritura mediante CQRS.
2. Fluent API para la configuración del modelo de persistencia.
3. Data Annotations exclusivamente para validar DTOs de entrada.
4. Inversión de dependencias mediante interfaces.
5. Migraciones y Seed Data para inicializar la base de datos.

---

# 🚀 Instalación

```bash
git clone <repo>
cd ProjectProposals_backend

dotnet restore

dotnet ef database update --project Infrastructure --startup-project ProjectApproval.Api

dotnet run --project ProjectApproval.Api
```

---

# 🚀 Mejoras Futuras

- JWT Authentication
- Refresh Tokens
- Unit Testing
- Integration Testing
- Logging centralizado
- Docker

---

# 👨‍💻 Autor

**Maximiliano Giménez**

**Full Stack Developer**

React • TypeScript • ASP.NET Core • SQL Server • Android (Jetpack Compose)
