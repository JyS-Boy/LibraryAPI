# 📚 Library API

API REST desarrollada con ASP.NET Core .NET 10 para la gestión de una biblioteca.

El sistema permite administrar usuarios, autores, categorías, libros, lectores y préstamos, implementando autenticación JWT, autorización basada en roles y persistencia mediante Entity Framework Core.

---

# 👨‍💻 Autor

**Jorge Luis Palomino García**

Proyecto desarrollado para el curso:

**.NET 10 – API Development**

---

# 🚀 Tecnologías Utilizadas

- .NET 10
- ASP.NET Core Minimal API
- Entity Framework Core
- SQL Server
- Docker
- JWT Authentication
- BCrypt.Net
- Mapster
- Swagger / OpenAPI

---

# 🏛 Arquitectura

```text
src
│
├── 01. Presentation
│   └── LibraryAPI
│
├── 02. Core
│   ├── Library.Business
│   └── Library.Common
│
├── 03. Repositories
│   └── Library.Repositories
│
├── 04. Infrastructure
│   └── Library.DataAccess
│
└── 05. Common
```

---

# 🔐 Roles Implementados

## Admin

- Crear usuarios
- Actualizar usuarios
- Eliminar usuarios
- Consultar usuarios

## Librarian

- Gestionar autores
- Gestionar categorías
- Gestionar libros
- Gestionar lectores
- Registrar préstamos
- Registrar devoluciones

---

# ⚠️ Importante: Creación del Primer Administrador

Por motivos de seguridad, el endpoint `POST /api/users` está protegido mediante autorización por rol Admin.

Comentar temporalmente la autorización del endpoint para crear el primer administrador y después volver a habilitarla.

```json
{
  "userName": "admin",
  "password": "Admin123*",
  "role": "Admin"
}
```

---

# ✅ Funcionalidades Implementadas

- JWT Authentication
- Autorización por Roles
- CRUD de Usuarios
- CRUD de Autores
- CRUD de Categorías
- CRUD de Libros
- CRUD de Lectores
- Gestión de Préstamos
- Gestión de Devoluciones
- Control automático de stock
- Loan Tracking
- Soft Delete
- Exception Handler Middleware

---

# 🌐 Endpoints

## Auth
- POST /api/auth/login

## Users
- POST /api/users
- GET /api/users?pageNumber=1&pageSize=10
- GET /api/users/{id}
- PUT /api/users/{id}
- DELETE /api/users/{id}

## Authors
- POST /api/authors
- GET /api/authors?pageNumber=1&pageSize=10
- GET /api/authors/{id}
- PUT /api/authors/{id}
- DELETE /api/authors/{id}

## Categories
- POST /api/categories
- GET /api/categories?pageNumber=1&pageSize=10
- GET /api/categories/{id}
- PUT /api/categories/{id}
- DELETE /api/categories/{id}

## Books
- POST /api/books
- GET /api/books?pageNumber=1&pageSize=10
- GET /api/books/{id}
- PUT /api/books/{id}
- DELETE /api/books/{id}

## Readers
- POST /api/readers
- GET /api/readers?pageNumber=1&pageSize=10
- GET /api/readers/{id}
- PUT /api/readers/{id}
- DELETE /api/readers/{id}

## Loans
- POST /api/loans
- GET /api/loans
- GET /api/loans/{id}
- POST /api/loans/{id}/return

---

# ▶️ Ejecución

```bash
docker compose up -d

dotnet ef database update

dotnet run
```

Abrir Swagger:

```text
https://localhost:{puerto}/swagger
```
---
# 📖 Documentación OpenAPI
La API se encuentra documentada mediante OpenAPI nativo de .NET 10.

La especificación puede consultarse en: `https://localhost:7096/openapi/v1.json`

La documentación incluye:

- Endpoints
- Request DTOs
- Response DTOs
- Códigos HTTP
- Tags
- Resúmenes de operaciones
---
# 🧪 Evidencia de pruebas
Las pruebas funcionales fueron realizadas mediante Postman.
- Login JWT
- Crear Libro
- Registrar Préstamo
- Registrar Devolución