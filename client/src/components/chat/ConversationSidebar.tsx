import { useEffect, useState } from "react";

import type { Conversation } from "../../types/conversation";

import { getConversations } from "../../features/chat/conversationService";
import ConversationItem from "./ConversationItem";

interface Props {
    selectedConversationId?: string;
    onConversationSelected: (conversationId: string) => void;
}

export default function ConversationSidebar({
    selectedConversationId,
    onConversationSelected
}: Props) {

    const [conversations, setConversations] =
        useState<Conversation[]>([]);

    useEffect(() => {
        loadConversations();
    }, []);

    async function loadConversations() {

        try {

            const data = await getConversations();

            setConversations(data);

            if (data.length > 0 && !selectedConversationId) {
                onConversationSelected(data[0].id);
            }

        }
        catch (error) {
            console.error(error);
        }
    }

    return (
        <div
            style={{
                borderRight: "1px solid #ddd",
                overflowY: "auto"
            }}
        >
            <h2 style={{ padding: 16 }}>
                Conversations
            </h2>

            {conversations.map(conversation => (
                <ConversationItem
                    key={conversation.id}
                    conversation={conversation}
                    selected={conversation.id === selectedConversationId}
                    onClick={() =>
                        onConversationSelected(conversation.id)
                    }
                />
            ))}
        </div>
    );
}