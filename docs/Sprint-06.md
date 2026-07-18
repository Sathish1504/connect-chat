# Sprint 06 - Chat Service Foundation

## Sprint Goal

Build the Chat Service as an independent microservice following Clean Architecture, Vertical Slice Architecture, and CQRS.

The Chat Service is responsible for all chat-related business logic and remains completely isolated from the Identity Service.

---

# Sprint Duration

Start:
YYYY-MM-DD

End:
YYYY-MM-DD

Branch:

```
feature/chat-service
```

---

# Objectives

- Create Chat Service
- Configure Clean Architecture
- Configure EF Core
- Configure SQL Server
- Configure Dependency Injection
- Configure Swagger
- Create Chat Database
- Design Chat Domain
- Implement Conversation Management
- Implement Messaging APIs

---

# Architecture

```
Presentation (Chat.API)
        │
        ▼
Application
        │
        ▼
Repository Interfaces
        │
        ▼
Infrastructure
        │
        ▼
EF Core
        │
        ▼
SQL Server
```

Architecture Patterns

- Clean Architecture
- Vertical Slice Architecture
- CQRS
- MediatR
- Repository Pattern
- Dependency Injection

---

# Solution Structure

```
services/
└── ChatService/
    ├── src/
    │   ├── Chat.API
    │   ├── Chat.Application
    │   ├── Chat.Domain
    │   └── Chat.Infrastructure
    │
    └── tests/
        └── Chat.Tests
```

---

# Technology Stack

Backend

- ASP.NET Core 10
- C#
- Entity Framework Core 10
- SQL Server
- MediatR
- FluentValidation
- Swagger

Architecture

- Clean Architecture
- Vertical Slice Architecture
- CQRS

---

# Database

Database Name

```
ConnectChatChatDb
```

Tables

```
Conversations

ConversationParticipants

Messages

__EFMigrationsHistory
```

---

# Domain Model

## Conversation

Properties

- Id
- Type
- Name
- CreatedBy
- CreatedAt

Navigation

- Participants
- Messages

---

## ConversationParticipant

Properties

- Id
- ConversationId
- UserId
- JoinedAt

---

## Message

Properties

- Id
- ConversationId
- SenderId
- Content
- Type
- Status
- CreatedAt
- EditedAt

---

# Enums

ConversationType

- Direct
- Group

MessageType

- Text
- Image
- File
- Audio
- Video

MessageStatus

- Sent
- Delivered
- Read

---

# Features Implemented

## Create Conversation

Endpoint

```
POST /api/conversations
```

Purpose

Creates a new direct or group conversation.

Status

✅ Completed

---

## Get Conversation

Endpoint

```
GET /api/conversations/{conversationId}
```

Purpose

Returns conversation details along with participants and messages.

Status

✅ Completed

---

## Send Message

Endpoint

```
POST /api/messages
```

Purpose

Sends a message to an existing conversation.

Business Rules

- Conversation must exist.
- Sender must be a participant.
- Message is stored with Sent status.

Status

✅ Completed

---

## Get Conversation Messages

Endpoint

```
GET /api/conversations/{conversationId}/messages
```

Purpose

Returns the complete conversation history ordered by CreatedAt.

Status

✅ Completed

---

# Repository Interfaces

Conversation Repository

```
IConversationRepository
```

Responsibilities

- Create Conversation
- Get Conversation
- Check Conversation Participants

---

Message Repository

```
IMessageRepository
```

Responsibilities

- Save Message
- Retrieve Conversation Messages

---

# Swagger APIs

Conversation APIs

```
POST /api/conversations

GET /api/conversations/{conversationId}
```

Message APIs

```
POST /api/messages

GET /api/conversations/{conversationId}/messages
```

---

# Validation

Implemented using FluentValidation.

Validation includes

- Required fields
- Enum validation
- Maximum content length
- Group conversation validation

---

# Project Structure

```
Chat.API

Endpoints

ConversationEndpoints

MessageEndpoints

------------------------

Chat.Application

Features

Conversations

CreateConversation

GetConversation

Messages

SendMessage

GetConversationMessages

Interfaces

Validators

------------------------

Chat.Domain

Entities

Enums

------------------------

Chat.Infrastructure

Persistence

Configurations

Repositories

DependencyInjection

ApplicationDbContext
```

---

# Testing

Successfully Tested

- Swagger
- SQL Server Persistence
- Conversation Creation
- Conversation Retrieval
- Message Sending
- Conversation History

Build Status

```
dotnet build

SUCCESS
```

Run Status

```
dotnet run

SUCCESS
```

---

# Deliverables

Completed

- Chat Service
- SQL Database
- EF Core Migrations
- Conversation APIs
- Messaging APIs
- CQRS
- MediatR
- Repository Pattern
- FluentValidation

---

# Lessons Learned

- Building a microservice using Clean Architecture.
- Implementing Vertical Slice Architecture.
- Applying CQRS with MediatR.
- Designing normalized chat database schemas.
- Implementing Repository Pattern.
- Using FluentValidation for request validation.
- Building RESTful APIs for chat functionality.

---

# Sprint Outcome

Sprint 06 successfully established the complete Chat Service foundation.

The service now supports:

- Conversation Management
- Message Management
- Persistent Chat Storage
- CQRS-based APIs
- Clean Architecture

The Chat Service is ready for real-time communication features.

---

# Next Sprint

Sprint 07

## Goals

- SignalR
- ChatHub
- JWT Authentication
- User Connection Management
- Join Conversation Groups
- Real-time Messaging
- Typing Indicators
- Online Presence
- Delivery Receipts
- Read Receipts

---

# Git

Branch

```
feature/chat-service
```

Suggested Commit

```bash
git add .

git commit -m "feat(chat): complete Sprint 06 chat service"

git push origin feature/chat-service
```

---

# Sprint Status

**Sprint 06 : ✅ COMPLETED**

Next Sprint

➡️ **Sprint 07 – Real-Time Messaging with SignalR**