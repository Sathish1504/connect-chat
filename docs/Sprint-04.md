# ConnectChat Enterprise

# Sprint 04 - Identity User Management & Security

## Sprint Goal

Complete the Identity Service user management capabilities by implementing secure authentication enhancements, profile management, refresh token rotation, logout functionality, and infrastructure improvements.

---

# Completed Features

## 1. Refresh Token API

### Endpoint

POST /api/auth/refresh

### Description

Allows authenticated users to obtain a new JWT Access Token using a valid Refresh Token without requiring another login.

### Features

- Refresh Token validation
- Refresh Token rotation
- New JWT generation
- New Refresh Token generation
- Automatic expiration validation
- Invalid token rejection
- Secure token replacement

---

## 2. Logout API

### Endpoint

POST /api/auth/logout

### Description

Logs out the authenticated user by revoking the stored Refresh Token.

### Features

- JWT Protected
- Uses CurrentUserService
- Revokes Refresh Token
- Invalidates future refresh requests
- Enterprise logout flow

---

## 3. Current User Service

Implemented

ICurrentUserService

CurrentUserService

### Responsibilities

- Current User Id
- User Name
- Email
- Authentication Status

Used by

- Logout
- Get Profile
- Update Profile
- Change Password

---

## 4. User Profile API

### Endpoint

GET /api/users/profile

### Features

Returns

- User Id
- User Name
- Email
- Profile Picture
- Email Confirmed
- Online Status
- Active Status
- Created Date
- Last Seen

Protected using JWT Authentication.

---

## 5. Update Profile API

### Endpoint

PUT /api/users/profile

### Features

Allows users to update

- User Name
- Profile Picture

Domain Method

User.UpdateProfile()

Automatically updates

UpdatedAt

---

## 6. Change Password API

### Endpoint

POST /api/users/change-password

### Features

- Current Password validation
- BCrypt password verification
- BCrypt password hashing
- Refresh Token revocation after password change
- Secure password update

Domain Method

User.ChangePassword()

---

# Security Improvements

Implemented

- Refresh Token Rotation
- Secure Logout
- Password Change
- Refresh Token Revocation
- Current User Service
- JWT Authorization
- Authorization Middleware

---

# Domain Improvements

Added

User.UpdateProfile()

User.ChangePassword()

User.RevokeRefreshToken()

These methods encapsulate business rules inside the Domain Entity.

---

# Repository Improvements

Added

GetByIdAsync()

Used by

- Logout
- Profile
- Change Password

---

# Validation

Implemented using FluentValidation

Update Profile

- User Name required
- User Name maximum length
- Optional Profile Picture

Change Password

- Current Password required
- New Password minimum length
- Confirm Password validation

---

# Infrastructure Improvements

Program.cs simplified

Created

Infrastructure

DependencyInjection.cs

Moved

- Repository registrations
- JWT registrations
- CurrentUserService
- Database registration

Created

SwaggerExtensions

JwtExtensions

Program.cs now only bootstraps the application.

---

# Controllers

AuthController

Endpoints

POST /api/auth/register

POST /api/auth/login

POST /api/auth/refresh

POST /api/auth/logout

GET /api/auth/me

UsersController

Endpoints

GET /api/users/profile

PUT /api/users/profile

POST /api/users/change-password

---

# Authentication Flow

Register

↓

Login

↓

JWT Access Token

↓

Refresh Token

↓

Protected APIs

↓

Refresh Token Rotation

↓

Logout

↓

Refresh Token Revoked

---

# Build Status

Build

Success

Run

Success

Swagger

Success

Authentication

Success

Authorization

Success

Profile APIs

Success

Password Change

Success

Logout

Success

Refresh Token Rotation

Success

---

# Project Structure

Identity.Application

Authentication

- Register
- Login
- Refresh
- Logout

Users

- GetProfile
- UpdateProfile
- ChangePassword

Identity.Domain

User Entity

Business Methods

Identity.Infrastructure

Authentication

CurrentUser

Repositories

Persistence

DependencyInjection

Identity.API

Controllers

Middleware

Extensions

---

# Enterprise Practices Followed

- Clean Architecture
- Vertical Slice Architecture
- CQRS
- MediatR
- FluentValidation
- Dependency Injection
- Global Exception Middleware
- JWT Authentication
- Refresh Token Rotation
- Domain Driven Design principles
- Secure Password Hashing using BCrypt
- Repository Pattern
- Enterprise Folder Structure

---

# Sprint 04 Outcome

Identity Service now supports

- Secure Authentication
- JWT Authorization
- Refresh Token Rotation
- Secure Logout
- Current User Service
- User Profile Management
- Password Management

Identity Service is approximately **90% complete**.

Remaining work

- Email Verification
- Password Reset
- SignalR Authentication Preparation

---

# Next Sprint

Sprint 05

Identity Security

- Email Verification
- Password Reset
- SignalR JWT Authentication
- Identity Service v1.0 Completion

After Sprint 05

Begin Chat Service

- SignalR
- One-to-One Messaging
- Group Chat
- Typing Indicators
- Read Receipts
- Presence
- Notifications