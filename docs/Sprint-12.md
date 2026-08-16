# ConnectChat Enterprise

# Sprint 12 Documentation

**Sprint Name:** User Discovery & New Chat

**Branch:** `feature/chat-service`

**Status:** ✅ Completed

**Sprint Duration:** Sprint 12

---

# Sprint Goal

Enable authenticated users to discover other registered users and start direct conversations from the UI.

---

# Objectives

- Browse registered users
- Search users
- Start a new chat
- Prevent duplicate direct conversations
- Automatically open created/existing conversation
- Display participant names instead of "Direct Chat"

---

# Architecture

This sprint follows the existing project architecture.

- Clean Architecture
- Vertical Slice Architecture
- CQRS
- MediatR
- Repository Pattern
- SOLID Principles

---

# Backend Changes

## Identity Service

### New Feature

```
Features
└── Users
    ├── GetUsers
    └── GetUserById
```

---

## Get Users

### Endpoint

```
GET /api/users
```

### Purpose

Returns all registered users except the currently logged-in user.

### Response

```json
[
    {
        "id": "guid",
        "userName": "Sathish",
        "email": "sathish@gmail.com",
        "isOnline": false
    }
]
```

---

## Get User By Id

### Endpoint

```
GET /api/users/{id}
```

### Purpose

Returns a single user's profile information.

### Response

```json
{
    "id": "guid",
    "userName": "Sathish",
    "email": "sathish@gmail.com",
    "profilePicture": null,
    "isOnline": false
}
```

---

# Chat Service

## Create Conversation

Enhanced existing conversation creation logic.

### Previous Behavior

Every request created a new conversation.

```
Demo
   ↓
Create
   ↓
Conversation A

Demo
   ↓
Create Again
   ↓
Conversation B
```

Result:

Duplicate conversations.

---

### Current Behavior

Before creating a conversation, the repository checks whether a direct conversation already exists between the two users.

If found:

```json
{
    "conversationId": "existing-guid",
    "message": "Conversation already exists."
}
```

Otherwise:

```json
{
    "conversationId": "new-guid",
    "message": "Conversation created successfully."
}
```

---

# Frontend Changes

## New Folder

```
features
└── users
      userService.ts
```

---

## User Service

Added:

- Get Users
- Get User By Id

---

## New Components

```
components
└── users
      UserPickerModal.tsx
      UserItem.tsx
```

---

# New Chat Flow

```
Login

↓

Dashboard

↓

New Chat

↓

User Picker

↓

Search Users

↓

Select User

↓

POST /api/conversations

↓

Conversation Created
OR
Existing Conversation Returned

↓

Refresh Conversation List

↓

Automatically Open Chat
```

---

# Dashboard Refactoring

Conversation state was moved from the sidebar to DashboardPage.

## Previous

```
ConversationSidebar

├── load conversations
├── own state
└── refresh
```

---

## Current

```
DashboardPage

├── conversations
├── selectedConversation
├── loadConversations()

↓

ConversationSidebar

↓

ChatWindow
```

Benefits

- Single Source of Truth
- Easier Refresh
- Better State Management
- Enterprise React Pattern

---

# Dynamic Conversation Names

Previously

```
Conversation.Name

↓

"Direct Chat"
```

Now

```
Conversation

↓

OtherParticipantId

↓

Identity Service

↓

UserName

↓

Sidebar
```

Result

Instead of

```
Direct Chat
```

Users now see

```
Demo
```

or

```
Sathish
```

depending on the selected conversation.

---

# Chat Header

Previous

```
Direct Chat
```

Current

```
Sathish
```

or

```
Demo
```

Header is synchronized with the selected conversation.

---

# Presence

Chat Header now displays

- Online
- Offline

using the existing PresenceContext.

---

# APIs Added

## Identity Service

### Get Users

```
GET /api/users
```

### Get User By Id

```
GET /api/users/{id}
```

---

## Chat Service

Enhanced

```
POST /api/conversations
```

Added duplicate conversation detection.

---

# Files Added

## Identity Service

```
Features
└── Users
    ├── GetUsers
    └── GetUserById
         Handler.cs
         Query.cs
         Response.cs
```

---

## Frontend

```
features
└── users
      userService.ts

components
└── users
      UserPickerModal.tsx
      UserItem.tsx
```

---

# Files Updated

## Backend

- UsersController
- IUserRepository
- UserRepository
- CreateConversationHandler

---

## Frontend

- DashboardPage
- ConversationSidebar
- ConversationItem
- ChatWindow
- conversationService
- conversation.ts

---

# Testing

## Identity

- Register
- Login
- Get Users
- Get User By Id

Passed

---

## Chat

- Create Conversation
- Prevent Duplicate Conversation
- Open Existing Conversation
- Load Messages

Passed

---

## Frontend

- User Picker
- Search Users
- New Chat
- Sidebar Refresh
- Auto Open Chat
- Header Synchronization

Passed

---

# Sprint Deliverables

| Feature | Status |
|----------|--------|
| Browse Users | ✅ |
| Search Users | ✅ |
| User Picker Modal | ✅ |
| New Chat Button | ✅ |
| Create Conversation | ✅ |
| Prevent Duplicate Conversations | ✅ |
| Auto Open Conversation | ✅ |
| Sidebar Refresh | ✅ |
| Dynamic Conversation Names | ✅ |
| Dynamic Chat Header | ✅ |
| Presence Integration | ✅ |
| Clean Architecture | ✅ |
| Vertical Slice | ✅ |
| CQRS | ✅ |
| Repository Pattern | ✅ |

---

# Sprint Outcome

Sprint 12 successfully introduced user discovery and direct chat creation, allowing authenticated users to start conversations with any registered user while preventing duplicate direct conversations.

The frontend was refactored to use DashboardPage as the single source of truth for conversation state, improving scalability and maintainability.

Dynamic participant name resolution was implemented using the Identity Service, replacing hardcoded conversation titles with actual user names in both the conversation list and chat header.

---

# Sprint Completion

**Sprint:** 12

**Status:** ✅ Completed

**Architecture:** Enterprise Ready

**Build Status:** ✅ Successful

**Ready for Sprint 13**

---

# Next Sprint

## Sprint 13 – Profile Pictures

Planned Features

- Upload Profile Picture
- Store Image Path
- Serve Static Files
- Sidebar Avatar
- Chat Header Avatar
- User Picker Avatar
- Profile Picture Management