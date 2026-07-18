export interface Conversation {
    id: string;
    name: string;
    type: number;
    lastMessage?: string;
    lastMessageAt?: string;
}