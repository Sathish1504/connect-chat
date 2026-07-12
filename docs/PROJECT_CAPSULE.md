# ConnectChat Enterprise - Project Capsule

You are my senior .NET architect and pair programmer.

We are building an enterprise-grade real-time communication platform called **ConnectChat** from scratch.

## Developer

Name: Sathish Kumar

GitHub:
https://github.com/Sathish1504/connect-chat

Current Branch:
feature/identity-auth

---

# Goal

Build a production-quality communication platform similar to Microsoft Teams/WhatsApp.

This project is intended to be my flagship portfolio project.

I want to learn while building.

Do NOT skip steps.

Explain architectural decisions.

Never generate large amounts of code without explaining it first.

---

# Tech Stack

Backend
- ASP.NET Core 10
- C#
- Clean Architecture
- CQRS
- Vertical Slice Architecture
- MediatR
- FluentValidation
- Entity Framework Core 10
- SQL Server
- BCrypt
- JWT
- SignalR
- WebRTC
- RabbitMQ
- Redis
- YARP API Gateway

Frontend
- React
- TypeScript
- Vite

Infrastructure
- Docker
- Docker Compose
- GitHub Actions
- Azure (later)

---

# Development Rules

Always build incrementally.

Every feature must:

1. Build successfully
2. Run successfully
3. Be tested
4. Be committed
5. Be pushed

Never continue with compilation errors.

---

# Git Workflow

main

feature/building-blocks

feature/identity-auth

feature/chat-service

feature/call-service

Every feature uses its own branch.

---

# Current Project Structure

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

---

# Completed

✔ Git Repository

✔ GitHub Repository

✔ Solution

✔ Identity Service

✔ SQL Server

✔ Entity Framework Core

✔ User Entity

✔ ApplicationDbContext

✔ Initial Migration

✔ Users Table

✔ Swagger

✔ MediatR

✔ Register Feature Structure

✔ Register API

✔ BCrypt Password Hashing

✔ Repository Pattern

✔ AuthController

✔ Register Endpoint Tested

✔ Git Commit

---

# Current Architecture

Identity.API

Controllers

Identity.Application

Features

Authentication

Register

Command.cs

Handler.cs

Validator.cs

Response.cs

Interfaces

Identity.Domain

Entities

User.cs

Identity.Infrastructure

Persistence

Repositories

Configurations

---

# User Entity

Fields:

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

Methods:

ConfirmEmail()

SetOnline()

ChangePassword()

UpdateProfilePicture()

---

# Database

SQL Server Express

Database

ConnectChatIdentityDb

Tables

Users

__EFMigrationsHistory

---

# Packages

EntityFrameworkCore

EntityFrameworkCore.SqlServer

EntityFrameworkCore.Design

MediatR

FluentValidation

BCrypt.Net

Swagger

---

# Coding Style

Use Vertical Slice Architecture.

Avoid unnecessary repositories.

Keep Program.cs clean.

Explain every architectural decision.

No shortcuts.

Always build production-quality code.

---

# Next Task

Sprint 3

Implement Login API.

Tasks:

Email lookup

Verify BCrypt password

Generate JWT Access Token

Generate Refresh Token

Store Refresh Token

Swagger Testing

Global Exception Middleware

Validation Pipeline

---

Continue exactly from this point.