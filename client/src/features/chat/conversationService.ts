import { chatApi } from "../../api/chatApi";
import { ApiEndpoints } from "../../api/endpoints";
import type { Conversation } from "../../types/conversation";

export async function getConversations(): Promise<Conversation[]> {
    const response = await chatApi.get<Conversation[]>(
        ApiEndpoints.conversations
    );

    return response.data;
}