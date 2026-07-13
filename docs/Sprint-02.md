# Sprint 02 - User Registration

## Project

**ConnectChat**

---

# Sprint Goal

Implement secure user registration using CQRS, MediatR and BCrypt password hashing.

---

# Objectives

- Configure MediatR
- Implement Vertical Slice Architecture
- Create Register Feature
- Add FluentValidation
- Hash Passwords
- Persist Users
- Test Register API

---

# Architecture

```
Client

↓

AuthController

↓

MediatR

↓

Register Command

↓

Register Handler

↓

Repository

↓

SQL Server
```

---

# Vertical Slice

```
Authentication

    Register

        Command

        Handler

        Validator

        Response
```

---

# CQRS

Command

```
Register.Command
```

Handler

```
Register.Handler
```

Validator

```
Register.Validator
```

Response

```
Register.Response
```

---

# Repository

Created

```
IUserRepository

UserRepository
```

Responsibilities

- Check Email Exists
- Add User
- Save Changes

---

# Password Security

Implemented BCrypt.

Flow

```
Password

↓

BCrypt Hash

↓

Database
```

Plain text passwords are never stored.

---

# Validation

Implemented FluentValidation.

Rules

```
UserName Required

Email Required

Valid Email

Password Minimum Length
```

---

# API

Endpoint

```
POST /api/Auth/register
```

Example Request

```json
{
  "userName": "demo",
  "email": "demo@gmail.com",
  "password": "Demo@123"
}
```

Example Response

```json
{
  "id": "...",
  "userName": "demo",
  "email": "demo@gmail.com"
}
```

---

# Packages

```
MediatR

FluentValidation

BCrypt.Net

Entity Framework Core
```

---

# Project Structure

```
Identity.Application

Features

    Authentication

        Register

            Command

            Handler

            Validator

            Response
```

---

# Testing

Verified

✅ Register API

✅ Duplicate Email Validation

✅ Password Hashing

✅ SQL Server Persistence

✅ Swagger Testing

---

# Sprint Outcome

Implemented a production-ready user registration workflow using:

- CQRS
- MediatR
- Vertical Slice Architecture
- FluentValidation
- BCrypt
- SQL Server

Users can now register securely.

---

# Git Commit

```
feat(identity): implement user registration with BCrypt
```

---

# Next Sprint

Sprint 03

Objectives

- Login API
- JWT Authentication
- Refresh Token
- Global Exception Middleware
- Validation Pipeline
- Protected APIs