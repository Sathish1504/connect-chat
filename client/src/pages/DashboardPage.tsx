import { useState } from "react";

import ConversationSidebar from "../components/chat/ConversationSidebar";
import ChatWindow from "../components/chat/ChatWindow";

export default function DashboardPage() {

    const [selectedConversationId, setSelectedConversationId] =
        useState<string>();

    return (
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
    );
}