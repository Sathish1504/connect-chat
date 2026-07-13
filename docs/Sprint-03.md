# Sprint 03 - Identity Authentication

## Project

**ConnectChat**

Enterprise-grade real-time communication platform built using ASP.NET Core 10, Clean Architecture, CQRS, Vertical Slice Architecture, React, SignalR, WebRTC, Redis, RabbitMQ, and Docker.

---

# Sprint Goal

Implement a secure authentication system that supports:

- User Login
- JWT Authentication
- Refresh Tokens
- Protected APIs
- Validation Pipeline
- Global Exception Handling

---

# Architecture

```
Client
    │
    ▼
AuthController
    │
    ▼
MediatR
    │
    ▼
Validation Pipeline
    │
    ▼
Command Handler
    │
    ▼
Repository
    │
    ▼
SQL Server
```

Authentication Flow

```
Register
      │
      ▼
BCrypt Password Hash
      │
      ▼
Database

Login
      │
      ▼
Verify Password
      │
      ▼
Generate JWT
      │
      ▼
Generate Refresh Token
      │
      ▼
Save Refresh Token
      │
      ▼
Return Tokens
```

---

# Features Implemented

## Authentication

### Register

- User Registration
- BCrypt Password Hashing
- Email Uniqueness Validation
- SQL Server Persistence

Endpoint

```
POST /api/Auth/register
```

---

### Login

Implemented secure login.

Features

- Email Lookup
- BCrypt Password Verification
- JWT Access Token Generation
- Refresh Token Generation
- Refresh Token Storage

Endpoint

```
POST /api/Auth/login
```

Response

```json
{
  "accessToken": "...",
  "refreshToken": "...",
  "expiresAt": "2026-07-12T12:56:13Z"
}
```

---

# JWT Authentication

Configured JWT Bearer Authentication.

Protected endpoints now require a valid JWT token.

Implemented

- Issuer Validation
- Audience Validation
- Lifetime Validation
- Signature Validation

Configuration

```json
"Jwt": {
  "Issuer": "ConnectChat",
  "Audience": "ConnectChatUsers",
  "SecretKey": "********",
  "AccessTokenExpirationMinutes": 30,
  "RefreshTokenExpirationDays": 7
}
```

---

# Protected Endpoint

Created authenticated endpoint.

```
GET /api/Auth/me
```

Requires

```
Authorization: Bearer <AccessToken>
```

Response

```json
{
  "userId": "...",
  "userName": "demo",
  "email": "demo@gmail.com"
}
```

---

# Refresh Token

User entity extended with

```
RefreshToken

RefreshTokenExpiryTime
```

Refresh token is stored securely in SQL Server after successful login.

---

# Validation

Implemented FluentValidation.

Validation automatically executes before handlers.

Pipeline

```
Controller
    │
    ▼
ValidationBehavior
    │
    ▼
Handler
```

Benefits

- No validation logic inside handlers
- Centralized validation
- Cleaner architecture

---

# Global Exception Middleware

Created

```
GlobalExceptionMiddleware
```

Responsibilities

- Handle Unauthorized Exceptions
- Handle Validation Exceptions
- Handle Bad Requests
- Handle Not Found
- Handle Internal Server Errors

Example Response

```json
{
    "status":400,
    "title":"Validation Failed",
    "errors":{
        "Email":[
            "'Email' must not be empty."
        ]
    }
}
```

---

# Security

Implemented

- BCrypt Password Hashing
- JWT Access Tokens
- Refresh Tokens
- Secure Token Validation
- Protected APIs
- Authorization Middleware

---

# Database Changes

Updated User table.

New columns

```
RefreshToken

RefreshTokenExpiryTime
```

Migration

```
AddRefreshTokenToUsers
```

---

# Project Structure

```
Identity.API

Controllers

Middleware

Identity.Application

Features
    Authentication
        Register
        Login

Behaviors

Interfaces

Identity.Domain

Entities

Identity.Infrastructure

Authentication

Persistence

Repositories
```

---

# Packages Used

```
ASP.NET Core Authentication JWT

Entity Framework Core

SQL Server

MediatR

FluentValidation

BCrypt.Net

Swagger
```

---

# Testing

Successfully Tested

✅ Register

✅ Login

✅ JWT Token Generation

✅ Refresh Token Storage

✅ Validation

✅ Unauthorized Response

✅ Protected Endpoint

```
GET /api/Auth/me
```

Verified JWT Authentication.

---

# Sprint Outcome

Completed a production-ready authentication module using:

- Clean Architecture
- CQRS
- Vertical Slice Architecture
- MediatR
- FluentValidation
- JWT
- Refresh Tokens
- SQL Server
- BCrypt

The Identity Service is now capable of securely authenticating users and protecting API endpoints.

---

# Git Commit

```
feat(identity): implement JWT authentication with refresh tokens
```

---

# Next Sprint

Sprint 04

Objectives

- Refresh Token API
- Logout API
- Revoke Refresh Token
- Current User Service
- User Profile API
- Update Profile
- Change Password
- Email Verification