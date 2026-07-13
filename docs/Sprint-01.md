# Sprint 01 - Project Foundation

## Project

**ConnectChat**

Enterprise-grade real-time communication platform inspired by Microsoft Teams and WhatsApp.

---

# Sprint Goal

Establish the project foundation following enterprise architecture principles.

---

# Objectives

- Create Git Repository
- Create GitHub Repository
- Create Solution Structure
- Implement Clean Architecture
- Configure SQL Server
- Configure Entity Framework Core
- Create Identity Service
- Configure Swagger
- Verify API Startup

---

# Architecture

```
ConnectChat

gateway/

services/

    IdentityService/

        Identity.API

        Identity.Application

        Identity.Domain

        Identity.Infrastructure

shared/

web/

docker/

docs/
```

---

# Clean Architecture

```
Presentation

↓

Application

↓

Domain

↓

Infrastructure
```

Responsibilities

Identity.API

- Controllers
- Dependency Injection
- Swagger
- Program.cs

Identity.Application

- Features
- Interfaces
- CQRS
- Validators

Identity.Domain

- Entities
- Business Rules

Identity.Infrastructure

- Database
- Entity Framework Core
- Repositories

---

# Entity Framework Core

Configured

- SQL Server Express
- ApplicationDbContext
- Dependency Injection

Connection String

```
ConnectChatIdentityDb
```

---

# Database

Created

```
ConnectChatIdentityDb
```

Tables

```
Users

__EFMigrationsHistory
```

---

# User Entity

Created initial User entity.

Properties

```
Id

UserName

Email

PasswordHash

ProfilePicture

IsOnline

EmailConfirmed

IsActive

CreatedAt

UpdatedAt

LastSeenAt
```

Methods

```
ConfirmEmail()

SetOnline()

ChangePassword()

UpdateProfilePicture()
```

---

# Packages

```
EntityFrameworkCore

EntityFrameworkCore.SqlServer

EntityFrameworkCore.Design

Swagger
```

---

# Project Structure

```
Identity.API

Controllers

Identity.Application

Identity.Domain

Entities

Identity.Infrastructure

Persistence

Repositories
```

---

# Testing

Verified

✅ API Starts Successfully

✅ Swagger Opens

✅ SQL Server Connection

✅ Database Creation

✅ Initial Migration

---

# Sprint Outcome

Completed the foundational architecture of ConnectChat using Clean Architecture and Entity Framework Core.

The project is now ready for application feature development.

---

# Git Commit

```
feat: initialize ConnectChat solution and Identity Service
```

---

# Next Sprint

Sprint 02

Objectives

- CQRS
- MediatR
- Register Feature
- BCrypt Password Hashing
- Repository Pattern
- Register API