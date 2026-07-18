# ConnectChat Enterprise
## Sprint 07 – Identity Service Authentication & Security

---

# Sprint Information

| Item | Details |
|------|----------|
| Sprint | Sprint 07 |
| Project | ConnectChat Enterprise |
| Duration | Sprint 07 |
| Branch | feature/identity-security |
| Technology | ASP.NET Core 10, C#, EF Core 10, SQL Server |
| Architecture | Clean Architecture + Vertical Slice Architecture |
| Pattern | CQRS + MediatR |

---

# Sprint Goal

Build a production-ready Identity Service responsible for authentication and authorization across the ConnectChat platform.

This sprint establishes the security foundation that every future microservice will rely on.

---

# Objectives

- Build Identity Microservice
- Implement Clean Architecture
- Configure EF Core
- Configure SQL Server
- Implement JWT Authentication
- Secure Password Storage
- Implement Refresh Tokens
- Email Verification
- Swagger JWT Authentication
- Global Exception Handling
- Current User Service

---

# Technology Stack

## Backend

- ASP.NET Core 10
- C#
- Entity Framework Core 10
- SQL Server
- JWT Bearer Authentication
- BCrypt
- MediatR
- FluentValidation

---

# Architecture

```
                Client
                   │
                   ▼
             Identity.API
                   │
         ┌─────────┴─────────┐
         ▼                   ▼
 Identity.Application   Identity.Infrastructure
         │                   │
         ▼                   ▼
    Identity.Domain      SQL Server
```

The Identity Service follows Clean Architecture principles.

Dependencies flow inward.

```
API
↓

Application
↓

Domain

Infrastructure
```

Infrastructure depends on Application.

Application depends on Domain.

API depends on Application + Infrastructure.

---

# Folder Structure

```
IdentityService
│
├── Identity.API
│
├── Identity.Application
│
├── Identity.Domain
│
├── Identity.Infrastructure
│
└── Identity.Tests
```

---

# Features Implemented

## User Registration

Implemented secure registration endpoint.

### Endpoint

```
POST /api/auth/register
```

### Responsibilities

- Validate request
- Check duplicate email
- Hash password
- Save user
- Return success response

---

## User Login

Implemented JWT authentication.

### Endpoint

```
POST /api/auth/login
```

Returns

- Access Token
- Refresh Token
- Expiration

---

## Refresh Token

Implemented Refresh Token workflow.

Endpoint

```
POST /api/auth/refresh
```

Allows users to receive new JWT tokens without logging in again.

---

## Logout

Implemented Logout endpoint.

Endpoint

```
POST /api/auth/logout
```

Refresh Token is revoked.

---

## Email Verification

Implemented

```
POST /api/auth/send-verification-email
```

Current implementation uses Console Email Service.

Future replacement:

- SMTP
- SendGrid
- Azure Communication Services

---

# JWT Authentication

Configured

```
Microsoft.AspNetCore.Authentication.JwtBearer
```

Authentication Pipeline

```
JWT

↓

Authentication Middleware

↓

Token Validation

↓

HttpContext.User

↓

Controllers / Endpoints
```

---

# JWT Claims

Generated claims

```
NameIdentifier

Name

Email

sub

jti

exp

iss

aud
```

---

# Password Security

Passwords are never stored in plain text.

Implemented

```
BCrypt
```

Workflow

```
Password

↓

Hash

↓

Database
```

Login

```
Entered Password

↓

BCrypt Verify

↓

Authentication
```

---

# CurrentUserService

Implemented

```
ICurrentUserService
```

Responsibilities

- Read authenticated user
- Extract UserId
- Provide current user to handlers

Implementation

```
HttpContextAccessor

↓

ClaimsPrincipal

↓

ClaimTypes.NameIdentifier

↓

Guid UserId
```

---

# Swagger Authentication

Configured Swagger Authorization.

Supports

```
Bearer JWT
```

Developers can authenticate directly from Swagger UI.

---

# Global Exception Middleware

Implemented centralized exception handling.

Responsibilities

- Capture exceptions
- Return ProblemDetails
- Prevent server crashes
- Consistent API responses

---

# Database

Database

```
ConnectChatIdentityDb
```

Main Table

```
Users
```

Fields

```
Id

UserName

Email

PasswordHash

RefreshToken

RefreshTokenExpiryTime

EmailConfirmed

IsActive

CreatedAt

UpdatedAt

LastSeenAt
```

---

# Entity Framework

Configured

```
ApplicationDbContext
```

Migration support

```
dotnet ef migrations add

dotnet ef database update
```

---

# CQRS

Implemented using MediatR.

Commands

```
RegisterCommand

LoginCommand

RefreshTokenCommand

LogoutCommand

SendVerificationEmailCommand
```

Queries

```
Current User

Profile

Authentication Validation
```

---

# Validation

Implemented FluentValidation.

Example

```
RegisterValidator

LoginValidator
```

Validation includes

- Email
- Password
- Username

---

# Repository Pattern

Implemented

```
IUserRepository
```

Concrete implementation

```
UserRepository
```

Responsibilities

- Create User
- Find by Email
- Find by Id
- Update User
- Save Refresh Token

---

# Dependency Injection

Infrastructure registers

```
ApplicationDbContext

UserRepository

JwtTokenGenerator

PasswordHasher

EmailService

CurrentUserService
```

---

# Security Improvements

Implemented

✔ Password Hashing

✔ JWT Authentication

✔ Refresh Tokens

✔ Email Verification

✔ Current User

✔ Secure Endpoints

✔ Swagger Authorization

---

# Testing

Successfully tested

Register

```
201 Created
```

Login

```
200 OK
```

Refresh Token

```
200 OK
```

Logout

```
200 OK
```

JWT Authorization

```
Authorized
```

Swagger

```
Authorize Button Working
```

---

# Challenges Faced

## EF Core Migration Issues

Resolved migration conflicts.

---

## JWT Configuration

Resolved

- Issuer
- Audience
- Secret Key

---

## Package Version Conflicts

Resolved package compatibility between

- ASP.NET Core 10
- EF Core 10
- Swashbuckle

---

## Current User

Implemented HttpContext based user resolution.

---

# Sprint Outcome

Successfully delivered a production-ready Identity Service.

Completed

- Authentication
- Authorization
- Password Security
- JWT
- Refresh Tokens
- Email Verification
- Current User
- Exception Middleware
- Swagger Authentication

The Identity Service now serves as the authentication provider for all future ConnectChat microservices.

---

# Next Sprint

Sprint 08

Goals

- Build Chat Microservice
- Conversation Management
- Message Storage
- SignalR Integration
- CQRS for Chat
- Repository Layer
- Real-Time Communication
- Chat Database
- Message APIs

---

# Git Commit Summary

Example

```
feat(identity): implement JWT authentication

feat(identity): add refresh token support

feat(identity): implement email verification

feat(identity): add BCrypt password hashing

feat(identity): configure swagger bearer authentication

feat(identity): implement CurrentUserService

feat(identity): add global exception middleware
```

---

# Sprint Status

✅ Completed Successfully

Identity Service is production-ready and prepared to support all remaining ConnectChat microservices.