export interface Message {

    id: string;

    senderId: string;

    content: string;

    type: number;

    status: number;

    createdAt: string;

    isPending?: boolean;
}