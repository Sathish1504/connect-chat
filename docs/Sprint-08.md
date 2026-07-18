# ConnectChat Enterprise
## Sprint 08 – Chat Service Foundation & Real-Time Communication

---

# Sprint Information

| Item | Details |
|------|----------|
| Sprint | Sprint 08 |
| Project | ConnectChat Enterprise |
| Service | Chat Service |
| Branch | feature/chat-service |
| Architecture | Clean Architecture + Vertical Slice Architecture |
| Pattern | CQRS + MediatR |

---

# Sprint Goal

Build the Chat microservice responsible for conversation management, message storage, and the foundation for real-time communication using SignalR.

This sprint establishes the messaging infrastructure that future features such as group chat, typing indicators, presence, and media sharing will rely on.

---

# Sprint Objectives

- Create Chat Microservice
- Configure Clean Architecture
- Configure SQL Server
- Implement Conversation Domain
- Implement Message Domain
- Implement Conversation Participants
- Configure Entity Framework Core
- Create Repository Layer
- Implement CQRS
- Create Chat APIs
- Integrate SignalR
- Prepare for JWT Authentication

---

# Technology Stack

## Backend

- ASP.NET Core 10
- C#
- EF Core 10
- SQL Server
- SignalR
- MediatR
- FluentValidation

---

# Architecture

```
                React Client
                     │
                     ▼
               Chat.API
                     │
         ┌───────────┴────────────┐
         ▼                        ▼
 Chat.Application         Chat.Infrastructure
         │                        │
         ▼                        ▼
     Chat.Domain            SQL Server
```

---

# Project Structure

```
ChatService

│
├── Chat.API
│
├── Chat.Application
│
├── Chat.Domain
│
├── Chat.Infrastructure
│
└── Chat.Tests
```

---

# Domain Model

## Conversation

Represents a chat session.

Fields

```
Id

Type

Name

CreatedBy

CreatedAt
```

Navigation Properties

```
Participants

Messages
```

---

## ConversationParticipant

Represents users participating in a conversation.

Fields

```
Id

ConversationId

UserId

JoinedAt
```

---

## Message

Represents chat messages.

Fields

```
Id

ConversationId

SenderId

Content

Type

Status

CreatedAt
```

---

# Enums

Implemented

## ConversationType

```
Direct

Group
```

---

## MessageType

```
Text

Image

File

Video

Audio
```

---

## MessageStatus

```
Sent

Delivered

Read
```

---

# Database

Database

```
ConnectChatChatDb
```

Tables

```
Conversations

ConversationParticipants

Messages
```

Relationships

```
Conversation

│

├── Participants

└── Messages
```

---

# Entity Framework Core

Configured

```
ApplicationDbContext
```

Includes

```
DbSet<Conversation>

DbSet<ConversationParticipant>

DbSet<Message>
```

Configured entity relationships using Fluent API.

---

# Repository Pattern

Implemented

## Conversation Repository

```
IConversationRepository

ConversationRepository
```

Responsibilities

- Create Conversation
- Get Conversation
- Validate Participant

---

## Message Repository

```
IMessageRepository

MessageRepository
```

Responsibilities

- Save Message
- Get Messages
- Query Conversation Messages

---

# CQRS

Implemented using MediatR.

---

## Commands

### CreateConversationCommand

Endpoint

```
POST /api/conversations
```

Responsibilities

- Create Conversation
- Add Participants
- Save Database

---

### SendMessageCommand

Endpoint

```
POST /api/messages
```

Responsibilities

- Validate Conversation
- Validate Sender
- Save Message

---

## Queries

### GetConversationMessagesQuery

Endpoint

```
GET /api/conversations/{conversationId}/messages
```

Returns

- Message History
- Sender
- Timestamp
- Status

---

# Minimal APIs

Implemented

```
ConversationEndpoints

MessageEndpoints
```

Advantages

- Lightweight
- Fast
- Simple Routing
- Minimal Boilerplate

---

# SignalR

Implemented ChatHub.

Hub

```
/hubs/chat
```

Methods

```
JoinConversation()

LeaveConversation()

SendMessageRealtime()
```

Lifecycle

```
OnConnectedAsync()

OnDisconnectedAsync()
```

---

# Hub Groups

Implemented

```
Conversation Groups
```

Each conversation has its own SignalR group.

Example

```
Conversation

↓

SignalR Group

↓

Broadcast
```

Only conversation participants receive messages.

---

# SignalR Flow

```
Client

↓

Connect

↓

JoinConversation()

↓

Group

↓

Broadcast

↓

ReceiveMessage
```

---

# Dependency Injection

Configured

```
ApplicationDbContext

ConversationRepository

MessageRepository
```

---

# Validation

Implemented FluentValidation.

Validators

```
CreateConversationValidator

SendMessageValidator
```

Validation

- ConversationId required
- Content required
- MessageType validation
- Participant validation

---

# Swagger Testing

Successfully tested

## Create Conversation

```
POST /api/conversations
```

Response

```
201 Created
```

---

## Send Message

```
POST /api/messages
```

Response

```
201 Created
```

---

## Get Messages

```
GET /api/conversations/{id}/messages
```

Response

```
200 OK
```

---

# Database Testing

Verified

Conversation created.

Participants stored.

Messages persisted.

Relationships validated.

---

# Challenges Faced

## EF Core Relationships

Configured one-to-many relationships between

- Conversation → Messages
- Conversation → Participants

---

## SignalR Configuration

Implemented hub registration.

Verified connections.

---

## CQRS Structure

Separated

Commands

Queries

Repositories

Handlers

---

## Message Validation

Ensured

Conversation exists.

Sender belongs to conversation.

---

# Sprint Outcome

Successfully delivered the Chat Service foundation.

Completed

✔ Conversation Management

✔ Message Storage

✔ Repository Layer

✔ CQRS

✔ SignalR Hub

✔ Real-Time Infrastructure

✔ SQL Server Integration

✔ Minimal APIs

The Chat Service is now ready for authentication integration and secure messaging.

---

# API Summary

## Conversations

```
POST /api/conversations
```

Create conversation.

---

## Messages

```
POST /api/messages
```

Send message.

---

```
GET /api/conversations/{conversationId}/messages
```

Retrieve conversation messages.

---

# Git Commit Summary

Example

```
feat(chat): create chat microservice

feat(chat): implement conversation domain

feat(chat): implement message domain

feat(chat): add SignalR hub

feat(chat): implement CQRS handlers

feat(chat): add conversation repository

feat(chat): add message repository

feat(chat): configure SQL Server

feat(chat): implement minimal APIs
```

---

# Next Sprint

Sprint 09

Objectives

- JWT Authentication
- Swagger Authorization
- CurrentUserService
- Secure SignalR
- Remove client-controlled identity
- Secure Message APIs
- Secure Conversation APIs
- React Authentication Integration

---

# Sprint Status

✅ Completed Successfully

The Chat Service foundation is complete and ready for enterprise-grade authentication and secure real-time messaging.