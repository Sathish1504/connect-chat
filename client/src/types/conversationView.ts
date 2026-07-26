import type { Conversation } from "./conversation";

export interface ConversationView extends Conversation {
    displayName: string;
}