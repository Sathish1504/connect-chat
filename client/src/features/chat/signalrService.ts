import {
    HubConnection,
    HubConnectionBuilder,
    HubConnectionState,
    LogLevel
} from "@microsoft/signalr";

import { tokenStorage } from "../../auth/tokenStorage";
import type { RealtimeMessage, MessageDeliveredEvent } from "../../types/realtimeMessage";
import type { MessageReadEvent } from "../../types/messageReadEvent";

class SignalRService {

    private connection?: HubConnection;

    async start(): Promise<HubConnection> {

        if (
            this.connection &&
            this.connection.state === HubConnectionState.Connected
        ) {
            return this.connection;
        }

        this.connection = new HubConnectionBuilder()
            .withUrl(
                "https://localhost:7290/hubs/chat",
                {
                    accessTokenFactory: () =>
                        tokenStorage.getAccessToken() ?? ""
                })
            .withAutomaticReconnect()
            .configureLogging(LogLevel.Information)
            .build();

        await this.connection.start();

        console.log("✅ SignalR Connected");

        return this.connection;
    }

    async joinConversation(
        conversationId: string
    ): Promise<void> {

        if (!this.connection)
            return;

        await this.connection.invoke(
            "JoinConversation",
            conversationId
        );
    }

    async leaveConversation(
        conversationId: string
    ): Promise<void> {

        if (!this.connection)
            return;

        await this.connection.invoke(
            "LeaveConversation",
            conversationId
        );
    }

    async startTyping(
        conversationId: string
    ): Promise<void> {

        if (!this.connection)
            return;

        await this.connection.invoke(
            "StartTyping",
            conversationId
        );

    }

    async stopTyping(
        conversationId: string
    ): Promise<void> {

        if (!this.connection)
            return;

        await this.connection.invoke(
            "StopTyping",
            conversationId
        );

    }

    async sendMessage(
        conversationId: string,
        content: string,
        type: number = 0
    ): Promise<void> {

        if (!this.connection)
            return;

        await this.connection.invoke(
            "SendMessageRealtime",
            {
                conversationId,
                content,
                type
            });
    }

    onReceiveMessage(
        handler: (message: RealtimeMessage) => void
    ): void {

        if (!this.connection)
            return;

        this.connection.off("ReceiveMessage");

        this.connection.on(
            "ReceiveMessage",
            (message: RealtimeMessage) => {

                handler(message);

            });
    }

    onUserTyping(
        handler: (data: {
            conversationId: string;
            userId: string;
            userName: string;
        }) => void
    ): void {

        if (!this.connection)
            return;

        this.connection.off("UserTyping");

        this.connection.on(
            "UserTyping",
            handler
        );

    }

    offUserTyping(): void {

        this.connection?.off(
            "UserTyping"
        );

    }

    onUserStoppedTyping(
        handler: (data: {
            conversationId: string;
            userId: string;
        }) => void
    ): void {

        if (!this.connection)
            return;

        this.connection.off(
            "UserStoppedTyping"
        );

        this.connection.on(
            "UserStoppedTyping",
            handler
        );

    }

    offUserStoppedTyping(): void {

        this.connection?.off(
            "UserStoppedTyping"
        );

    }

    offReceiveMessage(): void {

        this.connection?.off("ReceiveMessage");

    }
    onMessageDelivered(
        handler: (data: MessageDeliveredEvent) => void
    ): void {

        if (!this.connection)
            return;

        this.connection.off("MessageDelivered");

        this.connection.on(
            "MessageDelivered",
            handler
        );
    }

    offMessageDelivered(): void {

        this.connection?.off(
            "MessageDelivered"
        );

    }

    onMessageRead(
        handler: (data: MessageReadEvent) => void
    ): void {

        if (!this.connection)
            return;

        this.connection.off("MessageRead");

        this.connection.on(
            "MessageRead",
            handler
        );

    }

    offMessageRead(): void {

        this.connection?.off(
            "MessageRead"
        );

    }

    async stop(): Promise<void> {

        if (!this.connection)
            return;

        await this.connection.stop();

        this.connection = undefined;

    }
}

export const signalRService = new SignalRService();