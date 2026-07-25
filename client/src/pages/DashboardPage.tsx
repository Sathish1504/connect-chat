import { useEffect, useState } from "react";

import ConversationSidebar from "../components/chat/ConversationSidebar";
import ChatWindow from "../components/chat/ChatWindow";

import { presenceService } from "../features/presence/presenceService";

import { PresenceProvider } from "../features/presence/PresenceContext";

export default function DashboardPage() {

    const [selectedConversationId, setSelectedConversationId] =
        useState<string>();

    useEffect(() => {

        void presenceService.start();

    }, []);

    return (

        <PresenceProvider>

        <div
            style={{
                display: "grid",
                gridTemplateColumns: "320px 1fr",
                height: "100vh"
            }}
        >

            <ConversationSidebar
                selectedConversationId={selectedConversationId}
                onConversationSelected={setSelectedConversationId}
            />

            <ChatWindow
                conversationId={selectedConversationId}
            />

        </div>
        </PresenceProvider>

    );

}