export interface Conversation {
    id: string;
    name: string;
    type: number;

    otherParticipantId: string;

    lastMessage?: string;
    lastMessageAt?: string;
}