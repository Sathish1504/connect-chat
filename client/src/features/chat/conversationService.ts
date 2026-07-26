import { chatApi } from "../../api/chatApi";
import { ApiEndpoints } from "../../api/endpoints";
import type { Conversation } from "../../types/conversation";

export interface CreateConversationRequest {
    type: number;
    name: string;
    participantIds: string[];
}

export interface CreateConversationResponse {
    conversationId: string;
}

export async function getConversations(): Promise<Conversation[]> {

    const response = await chatApi.get<Conversation[]>(
        ApiEndpoints.conversations
    );

    return response.data;
}

export async function createConversation(
    request: CreateConversationRequest
): Promise<CreateConversationResponse> {

    const response = await chatApi.post<CreateConversationResponse>(
        ApiEndpoints.conversations,
        request
    );

    return response.data;
}