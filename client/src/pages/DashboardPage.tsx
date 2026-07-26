import { useEffect, useState } from "react";

import ConversationSidebar from "../components/chat/ConversationSidebar";
import ChatWindow from "../components/chat/ChatWindow";

import { getConversations } from "../features/chat/conversationService";

import { presenceService } from "../features/presence/presenceService";
import { PresenceProvider } from "../features/presence/PresenceContext";

import DashboardLayout from "../layouts/DashboardLayout";

import type { Conversation } from "../types/conversation";

import { getUserById } from "../features/users/userService";

export default function DashboardPage() {

    const [conversations, setConversations] = useState<
        (Conversation & { displayName: string })[]
    >([]);

    const [selectedConversationId, setSelectedConversationId] =
        useState<string>();

    useEffect(() => {

        void presenceService.start();

        void loadConversations();

    }, []);

    async function loadConversations() {

    try {

        const data = await getConversations();

        const mapped = await Promise.all(

            data.map(async conversation => {

                if (conversation.type === 1) {

                    const user = await getUserById(
                        conversation.otherParticipantId
                    );

                    return {

                        ...conversation,

                        displayName: user.userName

                    };

                }

                return {

                    ...conversation,

                    displayName:
                        conversation.name

                };

            })

        );

        setConversations(mapped);

        if (
            mapped.length > 0 &&
            !selectedConversationId
        ) {

            setSelectedConversationId(
                mapped[0].id
            );

        }

    }
    catch (error) {

        console.error(error);

    }

}

    return (

        <PresenceProvider>

            <DashboardLayout

                sidebar={

                    <ConversationSidebar
                        conversations={conversations}
                        selectedConversationId={selectedConversationId}
                        onConversationSelected={setSelectedConversationId}
                        onRefresh={loadConversations}
                    />

                }

                content={

                    <ChatWindow
    conversation={
        conversations.find(
            x => x.id === selectedConversationId
        )
    }
/>

                }

            />

        </PresenceProvider>

    );

}