import {
    HubConnection,
    HubConnectionBuilder,
    HubConnectionState,
    LogLevel
} from "@microsoft/signalr";

import { tokenStorage } from "../../auth/tokenStorage";
import type { RealtimeMessage } from "../../types/realtimeMessage";

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

    offReceiveMessage(): void {

        this.connection?.off("ReceiveMessage");

    }

    async stop(): Promise<void> {

        if (!this.connection)
            return;

        await this.connection.stop();

        this.connection = undefined;

    }
}

export const signalRService = new SignalRService();