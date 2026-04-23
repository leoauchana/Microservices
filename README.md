# 🚀 Microservices E-Commerce System (.NET)

<p align="center">
  <img src="https://raw.githubusercontent.com/dotnet/brand/main/logo/dotnet-logo.png" width="120" />
</p>

<p align="center">
  <b>Arquitectura de microservicios desarrollada en .NET</b><br/>
  Sistema backend orientado a e-commerce con enfoque en buenas prácticas, escalabilidad y desacoplamiento.
</p>

---

## 🟡 Estado del proyecto

🚧 **En desarrollo activo**
Este proyecto se encuentra en construcción y evolución constante.
Se irán incorporando nuevas funcionalidades, mejoras de arquitectura y optimizaciones.

---

## 🧠 Descripción

Este proyecto consiste en un sistema backend basado en **arquitectura de microservicios**, diseñado para simular un entorno real de e-commerce.

El objetivo principal es aplicar conceptos modernos de desarrollo backend como:

- Separación de responsabilidades
- Independencia de servicios
- Comunicación entre microservicios
- Escalabilidad horizontal
- Buenas prácticas de arquitectura

---

## 🧩 Arquitectura

El sistema está dividido en múltiples microservicios independientes, cada uno con su propia lógica de negocio y base de datos.

### 🔹 User Service

Responsable de la gestión de usuarios del sistema.

**Funcionalidades:**

- Registro de usuarios
- Consulta de usuarios
- Base para autenticación (futuro)

---

### 🔹 Product Service

Encargado de la gestión de productos.

**Funcionalidades:**

- Alta de productos
- Consulta de productos
- Gestión de stock

---

### 🔹 Order Service

Gestiona el proceso de compra.

**Funcionalidades:**

- Creación de pedidos
- Asociación con usuario y productos
- Validación mediante comunicación con otros servicios

---

## 🔗 Comunicación entre servicios

Los microservicios se comunican mediante:

- 🔹 HTTP
- 🔹 Eventos asincrónicos

---

## 🧰 Tecnologías utilizadas

- ⚙️ .NET (ASP.NET Core Web API)
- 🧠 Entity Framework Core
- 🐘 PostgreSQL
- 🐳 Docker & Docker Compose
- ⚡ Redis
- 📩 Sistema de mensajería
- 📄 Postman

---

## 🧱 Estructura del proyecto

```
Microservices/
 ├── UserService
 ├── ProductService
 ├── OrderService
```

Cada microservicio implementa una arquitectura basada en capas:

```
Src/
 ├── Api
 ├── Application
 ├── Domain
 ├── Data
 └── Transversal
```

---

## 🧠 Buenas prácticas aplicadas

- ✔ Arquitectura en capas
- ✔ Separación de dominios
- ✔ Base de datos por microservicio
- ✔ DTOs para comunicación
- ✔ Inyección de dependencias
- ✔ Manejo de errores centralizado
- ✔ Código desacoplado y mantenible

---

## 🐳 Ejecución con Docker

Próximamente se incluirá un `docker-compose.yml` para levantar:

- Microservicios
- Bases de datos PostgreSQL
- Redis
- Broker de mensajería

---

## 🚀 Objetivo del proyecto

Este proyecto fue creado con fines educativos y profesionales para:

- Consolidar conocimientos en backend con .NET
- Aprender arquitectura de microservicios
- Construir un portfolio sólido para el mundo laboral

---

## 📌 Próximas mejoras

- 🔐 Autenticación con JWT
- 🌐 API Gateway
- 📊 Logging centralizado
- 📦 Event Bus implementado
- 🧪 Tests automatizados

---

<p align="center">
  💡 <b>Proyecto en evolución constante</b> — enfocado en buenas prácticas y desarrollo profesional
</p>
