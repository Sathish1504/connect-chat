import { useEffect, useState } from "react";

import type { Message } from "../../types/message";
import type { RealtimeMessage } from "../../types/realtimeMessage";

import { useAuth } from "../../auth/AuthContext";
import { getMessages } from "../../features/chat/messageService";
import { signalRService } from "../../features/chat/signalrService";

import MessageInput from "./MessageInput";
import MessageList from "./MessageList";

interface Props {
    conversationId?: string;
}

export default function ChatWindow({
    conversationId
}: Props) {

    const { user } = useAuth();

    const [messages, setMessages] = useState<Message[]>([]);

    useEffect(() => {

        if (!conversationId)
            return;

        const activeConversationId = conversationId;

        let mounted = true;

        async function initialize() {

            try {

                // Load previous messages
                const history = await getMessages(
                    activeConversationId
                );

                if (mounted) {
                    setMessages(history);
                }

                // Connect SignalR
                await signalRService.start();

                // Join SignalR group
                await signalRService.joinConversation(
                    activeConversationId
                );

                // Listen for new messages
                signalRService.onReceiveMessage(
                    (message: RealtimeMessage) => {

                        console.log(
                            "📨 SignalR Message:",
                            message
                        );

                        if (!mounted)
                            return;

                        if (message.conversationId !== activeConversationId)
                            return;

                        const newMessage: Message = {

                            id: message.messageId,

                            senderId: message.senderId,

                            content: message.content,

                            type: message.type,

                            status: 0,

                            createdAt: new Date().toISOString()

                        };

                        setMessages(previous => {

                            const exists = previous.some(
                                x => x.id === newMessage.id
                            );

                            if (exists)
                                return previous;

                            return [
                                ...previous,
                                newMessage
                            ];

                        });

                    });

            }
            catch (error) {

                console.error(error);

            }

        }

        void initialize();

        return () => {

            mounted = false;

            signalRService.offReceiveMessage();

            void signalRService.leaveConversation(
                activeConversationId
            );

        };

    }, [conversationId]);

    async function handleSend(
        content: string
    ) {

        if (!conversationId)
            return;

        try {

            await signalRService.sendMessage(
                conversationId,
                content
            );

        }
        catch (error) {

            console.error(error);

        }

    }

    if (!conversationId) {

        return (

            <div
                style={{
                    display: "grid",
                    placeItems: "center",
                    height: "100%"
                }}
            >
                Select a conversation
            </div>

        );

    }

    return (

        <div
            style={{
                display: "flex",
                flexDirection: "column",
                height: "100%"
            }}
        >

            <div
                style={{
                    flex: 1,
                    overflowY: "auto",
                    padding: 20
                }}
            >

                <h2>Messages</h2>

                <MessageList
                    messages={messages}
                    currentUserId={user?.id ?? ""}
                />

            </div>

            <MessageInput
                onSend={handleSend}
            />

        </div>

    );

}