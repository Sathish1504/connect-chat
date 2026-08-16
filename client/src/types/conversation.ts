export interface Conversation {

    id: string;

    name: string;

    displayName: string;

    type: number;

    otherParticipantId: string;

    otherParticipantProfilePicture?: string | null;

    lastMessage?: string;

    lastMessageAt?: string;

}