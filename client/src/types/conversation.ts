export interface Conversation {

    id: string;

    name: string;

    displayName: string;

    type: number;

    otherParticipantId: string;

    lastMessage?: string;

    lastMessageAt?: string;

}