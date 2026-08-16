export type CallType = "audio" | "video";

export interface IncomingCall {
    callerId: string;
    targetUserId: string;
    conversationId: string;
    callType: CallType;
}