export interface RealtimeMessage {

    messageId: string;

    conversationId: string;

    senderId: string;

    content: string;

    type: number;

    status: number;
}

export interface MessageDeliveredEvent {

    conversationId: string;

    messageId: string;

    status: number;

}