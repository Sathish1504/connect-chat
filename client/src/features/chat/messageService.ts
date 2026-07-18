import { chatApi } from "../../api/chatApi";
import { ApiEndpoints } from "../../api/endpoints";
import type { Message } from "../../types/message";

export async function getMessages(
    conversationId: string
): Promise<Message[]> {

    const response = await chatApi.get<Message[]>(
        `${ApiEndpoints.conversations}/${conversationId}/messages`
    );

    return response.data;
}

export async function sendMessage(
    conversationId: string,
    content: string
) {
    return chatApi.post(ApiEndpoints.messages, {
        conversationId,
        content,
        type: 0
    });
}