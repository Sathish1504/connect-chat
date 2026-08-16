# ConnectChat Enterprise --- Sprint 13 Documentation

## Sprint Overview

**Project:** ConnectChat Enterprise\
**Sprint:** 13\
**Branch:** `feature/chat-service`\
**Sprint Focus:** Calling Signaling → WebRTC Audio Calling\
**Status:** In Progress\
**Date:** 2026-08-16

------------------------------------------------------------------------

## 1. Sprint Objective

Sprint 13 focuses on completing the first stage of real-time calling in
ConnectChat.

The calling signaling layer has been implemented using SignalR. The next
major objective is to establish an actual peer-to-peer audio connection
using WebRTC.

### Primary Goal

``` text
SignalR Calling Signaling
        ↓
WebRTC Audio Connection
        ↓
Stable Audio Call
        ↓
End Call / Cleanup
```

Video calling will only begin after audio calling is stable.

------------------------------------------------------------------------

## 2. Sprint 13 Status

  Feature                           Status
  --------------------------------- ----------------
  Call button UI                    ✅ Completed
  Audio call initiation             ✅ Completed
  Video call initiation signaling   ✅ Completed
  `CallUser` SignalR method         ✅ Completed
  `IncomingCall` event              ✅ Completed
  Incoming call modal               ✅ Completed
  Accept call                       ✅ Completed
  Reject call                       ✅ Completed
  CallingContext                    ✅ Completed
  Call type handling                ✅ Completed
  WebRTC audio                      🔄 In Progress
  WebRTC offer/answer               ⏳ Pending
  ICE candidate exchange            ⏳ Pending
  Active audio call UI              ⏳ Pending
  End-call signaling                ⏳ Pending
  Call cleanup                      ⏳ Pending
  Video calling                     ⏳ Future

------------------------------------------------------------------------

# 3. Existing Calling Architecture

The current architecture separates UI, calling state, SignalR signaling,
and the future WebRTC layer.

``` text
ChatHeader
    ↓
CallingContext
    ↓
callingService
    ↓
signalRService
    ↓
ChatHub
    ↓
Target User
```

WebRTC will be added after the signaling layer:

``` text
ChatHeader
    ↓
CallingContext
    ↓
WebRTC Service
    ↓
SignalR Signaling
    ↓
RTCPeerConnection
    ↓
Remote Audio
```

------------------------------------------------------------------------

# 4. Existing Frontend Calling Files

Current calling structure:

``` text
client/src/features/calling/

├── CallingContext.tsx
├── callingService.ts
│
├── components/
│   └── IncomingCallModal.tsx
│
└── types/
    └── call.ts
```

SignalR implementation:

``` text
client/src/features/chat/signalrService.ts
```

Important existing methods:

``` text
callUser()
onIncomingCall()
offIncomingCall()
```

The SignalR service also already handles:

``` text
start()
stop()

joinConversation()
leaveConversation()

startTyping()
stopTyping()

sendMessage()

onReceiveMessage()

onMessageDelivered()
onMessageRead()
```

------------------------------------------------------------------------

# 5. Existing Backend Calling Files

Backend:

``` text
services/ChatService/src/Chat.API/Hubs/

├── ChatHub.cs
├── HubGroups.cs
│
└── Contracts/
    ├── CallUserRequest.cs
    └── IChatClient.cs
```

`ChatHub` currently contains:

``` text
CallUser()
```

and sends:

``` text
IncomingCall
```

The client contract contains:

``` text
IncomingCall
```

------------------------------------------------------------------------

# 6. Current Calling Flow

## Caller

``` text
User clicks Phone / Video
        ↓
ChatHeader
        ↓
signalRService.callUser()
        ↓
ChatHub.CallUser()
        ↓
Target user's SignalR connection
        ↓
IncomingCall
```

## Receiver

``` text
IncomingCall
        ↓
CallingContext
        ↓
setIncomingCall()
        ↓
IncomingCallModal
        ↓
Accept / Reject
```

This signaling flow has already been tested successfully.

------------------------------------------------------------------------

# 7. Verified Calling Behavior

Development testing has confirmed:

``` text
SignalR Connected
        ↓
User Online Event
        ↓
Call initiated
        ↓
Incoming call received
        ↓
Accept call
        ↓
Reject call
```

Example browser console behavior:

``` text
📞 audio call started

Incoming call:
{
    callerId: "...",
    targetUserId: "...",
    conversationId: "...",
    callType: "audio"
}

Accept call
```

Therefore, the current blocker is no longer basic call signaling.

The next layer is WebRTC media negotiation.

------------------------------------------------------------------------

# 8. WebRTC Audio Objective

The first WebRTC milestone is:

``` text
Browser A microphone
        ↕
    WebRTC Peer
        ↕
Browser B microphone
```

Both users should be able to hear each other.

The implementation should use:

``` javascript
navigator.mediaDevices.getUserMedia({
    audio: true
})
```

and:

``` javascript
RTCPeerConnection
```

------------------------------------------------------------------------

# 9. WebRTC Components

Create:

``` text
client/src/features/calling/webrtcService.ts
```

The service should own:

-   `RTCPeerConnection`
-   local media stream
-   remote media stream
-   offer creation
-   answer creation
-   remote description
-   local description
-   ICE candidates
-   connection cleanup

Do not put WebRTC implementation directly inside `ChatHeader.tsx`.

------------------------------------------------------------------------

# 10. WebRTC Signaling Events

The SignalR layer should eventually support:

``` text
SendOffer
ReceiveOffer

SendAnswer
ReceiveAnswer

SendIceCandidate
ReceiveIceCandidate

EndCall
CallEnded
```

SignalR is only the signaling transport.

WebRTC carries the actual audio/video media.

------------------------------------------------------------------------

# 11. Audio Call Flow

## Caller

``` text
Click Audio Call
        ↓
CallUser
        ↓
Receiver accepts
        ↓
getUserMedia({ audio: true })
        ↓
Create RTCPeerConnection
        ↓
Add local audio track
        ↓
createOffer()
        ↓
setLocalDescription()
        ↓
SendOffer
```

## Receiver

``` text
ReceiveOffer
        ↓
Create RTCPeerConnection
        ↓
getUserMedia({ audio: true })
        ↓
Add local audio track
        ↓
setRemoteDescription()
        ↓
createAnswer()
        ↓
setLocalDescription()
        ↓
SendAnswer
```

## Caller receives answer

``` text
ReceiveAnswer
        ↓
setRemoteDescription()
```

## Both peers

``` text
onicecandidate
        ↓
SendIceCandidate
        ↓
ReceiveIceCandidate
        ↓
addIceCandidate()
```

------------------------------------------------------------------------

# 12. STUN Configuration

Development should use a STUN server.

Example:

``` text
stun:stun.l.google.com:19302
```

Conceptually:

``` typescript
const configuration = {
    iceServers: [
        {
            urls: "stun:stun.l.google.com:19302"
        }
    ]
};
```

Production should eventually use TURN as well.

------------------------------------------------------------------------

# 13. Active Call State

CallingContext should eventually manage states such as:

``` text
Idle
Calling
Ringing
Connecting
Connected
Rejected
Busy
Ended
Failed
```

A call state model should contain concepts such as:

``` text
callId
callerId
targetUserId
conversationId
callType
status
```

Media streams should remain inside the WebRTC/calling layer rather than
being unnecessarily passed through unrelated chat components.

------------------------------------------------------------------------

# 14. Active Audio Call UI

Create an active-call component later:

``` text
client/src/features/calling/components/ActiveCallModal.tsx
```

Initial audio UI:

``` text
┌───────────────────────────────┐
│                               │
│          Profile Avatar       │
│                               │
│             User              │
│            02:31               │
│                               │
│       🎤       🔊       📞     │
│                               │
└───────────────────────────────┘
```

Required controls:

-   Mute/unmute microphone
-   Speaker state
-   End call
-   Connection status

------------------------------------------------------------------------

# 15. End Call

Implement:

``` text
EndCall
CallEnded
```

Flow:

``` text
User clicks End Call
        ↓
SignalR EndCall
        ↓
Remote user receives CallEnded
        ↓
Close RTCPeerConnection
        ↓
Stop local media tracks
        ↓
Clear remote stream
        ↓
Reset call state
        ↓
Return to chat
```

Cleanup must include:

``` javascript
stream.getTracks().forEach(track => track.stop());
```

and closing the peer connection.

------------------------------------------------------------------------

# 16. WebRTC Error Handling

Handle:

``` text
Microphone permission denied
No microphone available
ICE failure
Connection failure
Remote user disconnected
Call rejected
Call ended
Network interruption
Browser does not support WebRTC
```

The UI should provide useful messages instead of silently failing.

------------------------------------------------------------------------

# 17. Testing Plan

Use two browser sessions.

Recommended:

``` text
Chrome
    ↓
Sathish

Edge
    ↓
demo
```

Test:

### Test 1 --- Call initiation

``` text
Sathish → demo
```

Expected:

``` text
Incoming call appears on demo
```

### Test 2 --- Accept

``` text
demo → Accept
```

Expected:

``` text
WebRTC negotiation begins
```

### Test 3 --- Microphone

Expected:

``` text
Sathish can hear demo
demo can hear Sathish
```

### Test 4 --- Reject

Expected:

``` text
Caller receives rejected state
```

### Test 5 --- End

Expected:

``` text
Both users return to normal chat
Microphones are released
RTCPeerConnection is closed
```

### Test 6 --- Reconnect

Disconnect/reconnect the browser and verify that SignalR reconnects
without corrupting the calling state.

------------------------------------------------------------------------

# 18. Presence Architecture

Presence is already implemented.

Files:

``` text
Chat.API/Presence/IPresenceTracker.cs
Chat.API/Presence/InMemoryPresenceTracker.cs
Chat.API/Hubs/PresenceHub.cs
```

Current tracker supports multiple connections per user.

Concept:

``` text
User
 ├── Chrome connection
 ├── Edge connection
 └── Mobile connection
```

The user remains online until all connections are disconnected.

Registration:

``` csharp
builder.Services.AddSingleton<
    IPresenceTracker,
    InMemoryPresenceTracker>();
```

Presence hub:

``` text
/hubs/presence
```

Events:

``` text
UserOnline
UserOffline
```

------------------------------------------------------------------------

# 19. Important Presence Rule

Do not replace the working `_connections` presence dictionary
unnecessarily.

If chat-specific connections are required, keep them clearly separated.

For example:

``` text
_connections
    ↓
Presence connections

_chatConnections
    ↓
Chat/calling-specific connections
```

Both must be explicitly declared before use.

Avoid errors such as:

``` text
CS0103:
The name '_chatConnections' does not exist
```

------------------------------------------------------------------------

# 20. Current Chat Architecture

Conversation APIs:

``` text
POST /api/conversations
POST /api/messages
GET /api/conversations/{conversationId}/messages
```

Realtime:

``` text
/hubs/chat
```

Conversation groups:

``` text
conversation:{conversationId}
```

Realtime message:

``` text
ReceiveMessage
```

Typing:

``` text
UserTyping
UserStoppedTyping
```

Delivery:

``` text
MessageDelivered
```

Read:

``` text
MessageRead
```

------------------------------------------------------------------------

# 21. Current Frontend Dashboard

Main files:

``` text
client/src/pages/DashboardPage.tsx
client/src/layouts/DashboardLayout.tsx
client/src/components/chat/ConversationSidebar.tsx
client/src/components/chat/ChatWindow.tsx
client/src/components/chat/header/ChatHeader.tsx
```

Current layout:

``` text
┌────────────────────────────────────────────┐
│ Conversation Sidebar │ Chat Window         │
│                       │                    │
│ Search                │ Chat Header        │
│ Conversations         │                    │
│                       │ Messages           │
│ User profile          │                    │
│                       │ Message Input      │
└────────────────────────────────────────────┘
```

------------------------------------------------------------------------

# 22. Profile Integration

Sidebar currently displays:

``` text
ProfileAvatar
UserName
My Profile
```

Profile data is loaded by:

``` text
getProfile()
```

Dashboard passes:

``` text
profilePicture
userName
```

to:

``` text
ConversationSidebar
```

------------------------------------------------------------------------

# 23. Build Status

Frontend production build has been successfully passing:

``` powershell
cd S:\Project\ConnectChat\client
npm run build
```

Backend Chat API has also been building successfully:

``` powershell
cd S:\Project\ConnectChat\services\ChatService\src\Chat.API
dotnet build
```

Rule:

> Never move forward with a broken build.

------------------------------------------------------------------------

# 24. Sprint 13 Definition of Done

Sprint 13 WebRTC audio is complete only when:

-   [ ] WebRTC peer connection created
-   [ ] Microphone permission handled
-   [ ] Local audio track added
-   [ ] Offer generated
-   [ ] Offer transmitted through SignalR
-   [ ] Answer generated
-   [ ] Answer transmitted through SignalR
-   [ ] ICE candidates exchanged
-   [ ] Remote audio received
-   [ ] Audio call UI displayed
-   [ ] Mute/unmute works
-   [ ] End call works
-   [ ] Media tracks cleaned up
-   [ ] Peer connection cleaned up
-   [ ] Reject works
-   [ ] Error states handled
-   [ ] Chrome ↔ Edge tested
-   [ ] Frontend build passes
-   [ ] Backend build passes
-   [ ] Git commit created
-   [ ] Git push completed

------------------------------------------------------------------------

# 25. Sprint 13 Future Work After Audio

Once audio is stable:

## Phase 2 --- Video

``` text
getUserMedia({
    audio: true,
    video: true
})
```

Add:

-   Local video
-   Remote video
-   Camera toggle
-   Full screen
-   Picture-in-picture
-   Video call UI

------------------------------------------------------------------------

## Phase 3 --- Call Reliability

Add:

-   Reconnecting state
-   Network failure handling
-   ICE restart
-   Busy state
-   Missed call
-   Call timeout
-   Call history

------------------------------------------------------------------------

## Phase 4 --- Production Calling

Add:

-   TURN server
-   Secure TURN credentials
-   Call metrics
-   Connection quality
-   Distributed signaling
-   Redis
-   Scale-out SignalR

------------------------------------------------------------------------

# 26. Future Sprint Roadmap

## Sprint 14 --- Advanced Chat

-   Unread counts
-   Message search
-   Reply
-   Edit
-   Delete
-   Reactions
-   Pinned messages
-   Starred messages

## Sprint 15 --- Media

-   Images
-   Videos
-   Documents
-   Audio messages
-   Attachments
-   Preview
-   Object storage
-   Thumbnails

## Sprint 16 --- Group Chat

-   Groups
-   Group avatar
-   Group members
-   Admin
-   Owner
-   Permissions
-   Group settings

## Sprint 17 --- Notifications

-   Browser notifications
-   Push notifications
-   Notification center
-   Email notifications

## Sprint 18 --- Security

-   2FA
-   OTP
-   Passkeys
-   Rate limiting
-   Device management
-   Audit logs
-   Session management

## Sprint 19 --- Scalability

-   Redis
-   RabbitMQ
-   SignalR scale-out
-   Distributed presence
-   Background workers
-   Caching

## Sprint 20 --- Enterprise

-   Organizations
-   Teams
-   Roles
-   Permissions
-   Admin dashboard
-   Security policies
-   Retention policies
-   Audit dashboard

## Sprint 21 --- Production

-   Docker
-   Docker Compose
-   GitHub Actions
-   Azure
-   Monitoring
-   Logging
-   Metrics
-   Tracing
-   Health checks
-   Secrets management

------------------------------------------------------------------------

# 27. Long-Term Product Architecture

``` text
                         CONNECTCHAT
                              │
                         API Gateway
                            YARP
                              │
          ┌───────────────────┼───────────────────┐
          │                   │                   │
          ▼                   ▼                   ▼
     Identity Service    Chat Service       Future Services
          │                   │
          ▼                   ▼
      Identity DB          Chat DB
                              │
                 ┌────────────┴────────────┐
                 ▼                         ▼
               Redis                    RabbitMQ
                 │                         │
                 ▼                         ▼
             Presence              Notifications
                 │
                 ▼
              SignalR
                 │
                 ▼
              WebRTC
              /     \
           STUN     TURN
```

------------------------------------------------------------------------

# 28. Engineering Rules

1.  Preserve existing working functionality.
2.  Do not rewrite working architecture without a clear reason.
3.  Keep WebRTC isolated from chat UI.
4.  Keep SignalR responsible for signaling, not media transport.
5.  Keep authentication centralized.
6.  Keep presence state separate from call state.
7.  Use DTOs/contracts for SignalR payloads.
8.  Validate all incoming requests.
9.  Avoid unnecessary global state.
10. Dispose/cleanup WebRTC resources correctly.
11. Test with two authenticated users.
12. Build after meaningful changes.
13. Commit stable milestones.
14. Never ignore compiler errors.
15. Never implement video before audio is stable.

------------------------------------------------------------------------

# 29. Immediate Next Command Sequence

Start the next development session with:

``` powershell
cd S:\Project\ConnectChat

Get-Content .\client\src\features\calling\CallingContext.tsx

Get-Content .\client\src\features\calling\callingService.ts

Get-Content .\client\src\features\calling\types\call.ts

Get-Content .\client\src\features\calling\components\IncomingCallModal.tsx

Get-Content .\client\src\features\chat\signalrService.ts

Get-Content .\services\ChatService\src\Chat.API\Hubs\ChatHub.cs

Get-Content .\services\ChatService\src\Chat.API\Hubs\Contracts\IChatClient.cs

Get-ChildItem .\services\ChatService\src\Chat.API\Hubs\Contracts -File |
    Select-Object Name
```

Then:

``` text
Review current calling code
        ↓
Add WebRTC signaling contracts
        ↓
Add WebRTC service
        ↓
Implement audio offer
        ↓
Implement answer
        ↓
Implement ICE
        ↓
Receive remote audio
        ↓
Implement end call
        ↓
Test Chrome ↔ Edge
        ↓
Build frontend
        ↓
Build backend
        ↓
Git commit
        ↓
Git push
```

------------------------------------------------------------------------

# 30. Sprint 13 Final Target

The final Sprint 13 target is:

``` text
┌───────────────┐
│    Sathish    │
│   🎤 Audio    │
└───────┬───────┘
        │
        │ WebRTC
        │
        ▼
┌───────────────┐
│     demo      │
│   🔊 Audio    │
└───────────────┘
```

with:

``` text
SignalR
  = Authentication + Calling Signaling

WebRTC
  = Actual Audio Media

STUN/TURN
  = Network Traversal
```

------------------------------------------------------------------------

## Sprint 13 Summary

**Completed:** Calling signaling, incoming call, accept/reject, presence
integration.

**Current focus:** WebRTC one-to-one audio calling.

**Next:** WebRTC video calling.

**Long-term:** Build ConnectChat into a scalable enterprise
communication platform with messaging, calling, media, groups,
notifications, security, distributed infrastructure, and production
deployment.
