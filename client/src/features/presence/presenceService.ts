import {
    HubConnection,
    HubConnectionBuilder,
    HubConnectionState,
    LogLevel
} from "@microsoft/signalr";

import { tokenStorage } from "../../auth/tokenStorage";

class PresenceService {

    private connection?: HubConnection;

    async start(): Promise<void> {

        if (
            this.connection &&
            this.connection.state === HubConnectionState.Connected
        ) {
            return;
        }

        this.connection =
            new HubConnectionBuilder()

                .withUrl(
                    "https://localhost:7290/hubs/presence",
                    {
                        accessTokenFactory: () =>
                            tokenStorage.getAccessToken() ?? ""
                    })

                .withAutomaticReconnect()

                .configureLogging(LogLevel.Information)

                .build();

        this.connection.onreconnected(() => {

            console.log("🟢 Presence Reconnected");

        });

        await this.connection.start();

        console.log("🟢 Presence Connected");

    }

    async getOnlineUsers(): Promise<string[]> {

        if (!this.connection)
            return [];

        return await this.connection.invoke<string[]>(
            "GetOnlineUsers"
        );

    }

    onUserOnline(
        handler: (userId: string) => void
    ): void {

        this.connection?.off("UserOnline");

        this.connection?.on(
            "UserOnline",
            handler
        );

    }

    onUserOffline(
        handler: (userId: string) => void
    ): void {

        this.connection?.off("UserOffline");

        this.connection?.on(
            "UserOffline",
            handler
        );

    }

    offUserOnline(): void {

        this.connection?.off(
            "UserOnline"
        );

    }

    offUserOffline(): void {

        this.connection?.off(
            "UserOffline"
        );

    }

}

export const presenceService =
    new PresenceService();