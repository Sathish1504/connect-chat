import { useEffect, useRef, useState } from "react";

import type { Message } from "../../types/message";
import type { RealtimeMessage } from "../../types/realtimeMessage";

import { useAuth } from "../../auth/AuthContext";
import {
    getMessages,
    markConversationDelivered,
    markConversationRead
} from "../../features/chat/messageService";
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

    const [typingUser, setTypingUser] = useState<string | null>(null);

    const bottomRef = useRef<HTMLDivElement>(null);

    useEffect(() => {

        bottomRef.current?.scrollIntoView({
            behavior: "smooth"
        });

    }, [messages]);

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

                await markConversationDelivered(
                    activeConversationId
                );

                await markConversationRead(
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

                            status: message.status,

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


                        if (message.senderId !== user?.id) {

                            void markConversationDelivered(
                                activeConversationId
                            );

                            void markConversationRead(
                                activeConversationId
                            );

                        }

                    });

                signalRService.onUserTyping(data => {

                    if (!mounted)
                        return;

                    if (data.conversationId !== activeConversationId)
                        return;

                    if (data.userId === user?.id)
                        return;

                    setTypingUser(data.userName);

                });

                signalRService.onUserStoppedTyping(data => {

                    if (!mounted)
                        return;

                    if (data.conversationId !== activeConversationId)
                        return;

                    setTypingUser(null);

                });

                signalRService.onMessageDelivered(data => {

                    if (!mounted)
                        return;

                    if (data.conversationId !== activeConversationId)
                        return;

                    setMessages(previous =>
                        previous.map(message =>

                            message.id === data.messageId
                                ? {
                                    ...message,
                                    status: data.status
                                }
                                : message

                        )
                    );

                });

                signalRService.onMessageRead(data => {

                    if (!mounted)
                        return;

                    if (data.conversationId !== activeConversationId)
                        return;

                    setMessages(previous =>
                        previous.map(message => {

                            if (message.id === data.messageId) {

                                return {
                                    ...message,
                                    status: data.status
                                };

                            }

                            return message;

                        })
                    );

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
            signalRService.offUserTyping();
            signalRService.offUserStoppedTyping();
            signalRService.offMessageDelivered();
            signalRService.offMessageRead();

            void signalRService.leaveConversation(
                activeConversationId
            );

        };

    }, [conversationId, user?.id]);

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
    async function handleTypingStart() {

        if (!conversationId)
            return;

        try {

            await signalRService.startTyping(
                conversationId
            );

        }
        catch (error) {

            console.error(error);

        }

    }

    async function handleTypingStop() {

        if (!conversationId)
            return;

        try {

            await signalRService.stopTyping(
                conversationId
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
                    ref={bottomRef}
                    messages={messages}
                    currentUserId={user?.id ?? ""}
                />

            </div>

            {typingUser && (

                <div
                    style={{
                        padding: "0 20px 10px",
                        fontSize: 13,
                        color: "#666",
                        fontStyle: "italic"
                    }}
                >
                    {typingUser} is typing...
                </div>

            )}

            <MessageInput
                onSend={handleSend}
                onTypingStart={handleTypingStart}
                onTypingStop={handleTypingStop}
            />

        </div>

    );

}