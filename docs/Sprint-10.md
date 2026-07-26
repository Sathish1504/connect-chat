# ConnectChat Enterprise
# Sprint 10 – Real-Time Chat, Presence & Typing Indicators

## Sprint Goal

Implement enterprise-grade real-time communication using SignalR, enabling users to exchange messages instantly, view online presence, and receive typing indicators.

---

# Objectives

- Implement SignalR Chat Hub
- Join and leave conversation groups
- Real-time message delivery
- Online Presence Hub
- Typing Indicator
- React SignalR integration
- Conversation sidebar improvements
- Online user tracking

---

# Backend Features

## ChatHub

Created:

- JoinConversation()
- LeaveConversation()
- SendMessageRealtime()
- StartTyping()
- StopTyping()

SignalR Groups are based on:

```csharp
conversation-{conversationId}
```

---

## Presence Hub

Implemented:

- User Connected
- User Disconnected
- Online User List
- Presence Tracker

Services:

```
IPresenceTracker
InMemoryPresenceTracker
PresenceHub
```

---

## SignalR Authentication

Configured JWT authentication for SignalR.

```
access_token
```

is read from query string.

---

## Message Broadcasting

Implemented

```
ReceiveMessage
```

event

Broadcasts to every participant inside the conversation group.

---

## Typing Indicator

Events

```
UserTyping
UserStoppedTyping
```

broadcast to

```
OthersInGroup()
```

---

# REST API

Implemented

GET

```
/api/conversations
```

Returns

- Conversation Id
- Name
- Type
- Other Participant
- Last Message
- Last Message Time

---

GET

```
/api/conversations/{conversationId}/messages
```

Returns full conversation history.

---

POST

```
/api/messages
```

Creates a new message.

---

# React Frontend

Implemented

## ChatWindow

Responsibilities

- Load messages
- Connect SignalR
- Join conversation
- Receive new messages
- Auto-scroll
- Display typing indicator

---

## Conversation Sidebar

Displays

- Conversation list
- Last message
- Selected conversation

---

## Message Components

Created

```
MessageList
MessageBubble
MessageInput
ConversationItem
ConversationSidebar
```

---

## SignalR Client

Implemented

```
signalRService.ts
```

Supports

- Start Connection
- Join Conversation
- Leave Conversation
- Send Message
- Receive Message
- Start Typing
- Stop Typing

---

## Presence Context

Created

```
PresenceProvider
```

Supports

- Online Users
- Offline Users
- React Context API

---

# Architecture

Backend

ASP.NET Core 10

SignalR

CQRS

MediatR

Clean Architecture

Vertical Slice

Repository Pattern

---

Frontend

React

TypeScript

Vite

SignalR Client

Context API

---

# Challenges Solved

✔ SignalR JWT Authentication

✔ Conversation Groups

✔ Duplicate Messages

✔ Presence Tracking

✔ Typing Indicators

✔ Conversation Sidebar

✔ React SignalR Lifecycle

---

# Deliverables

✅ Real-time Messaging

✅ Online Presence

✅ Typing Indicator

✅ Conversation List

✅ SignalR Integration

---

# Sprint Outcome

Users can

- Chat in real time
- View conversations
- See online users
- Receive typing notifications
- Join conversation groups automatically

---

# Next Sprint

Sprint 11

Message Status

- Sent
- Delivered
- Read Receipts

Enterprise Notification Pipeline