import { useEffect, useState } from "react";

import ConversationSidebar from "../components/chat/ConversationSidebar";
import ChatWindow from "../components/chat/ChatWindow";

import { getConversations } from "../features/chat/conversationService";

import { presenceService } from "../features/presence/presenceService";
import { PresenceProvider } from "../features/presence/PresenceContext";

import DashboardLayout from "../layouts/DashboardLayout";

import type { Conversation } from "../types/conversation";

import { getUserById } from "../features/users/userService";

import { getProfile } from "../features/profile/profileService";

import { useAuth } from "../auth/AuthContext";

export default function DashboardPage() {

    const { user } = useAuth();

    const [conversations, setConversations] = useState<
        (Conversation & { displayName: string })[]
    >([]);

    const [selectedConversationId, setSelectedConversationId] =
        useState<string>();

    const [profilePicture, setProfilePicture] =
        useState<string | null>(null);

    useEffect(() => {

        void presenceService.start();

        void loadConversations();
        void loadProfile();

    }, []);

    async function loadProfile() {

        try {

            const profile = await getProfile();

            setProfilePicture(
                profile.profilePicture
            );

        }
        catch (error) {

            console.error(
                "Failed to load profile:",
                error
            );

        }

    }

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
                            displayName: user.userName,
                            otherParticipantProfilePicture:
                                user.profilePicture ?? null
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
                        selectedConversationId={
                            selectedConversationId
                        }
                        onConversationSelected={
                            setSelectedConversationId
                        }
                        onRefresh={loadConversations}
                        profilePicture={profilePicture}
                        userName={user?.username}
                    />

                }

                content={

                    <ChatWindow
                        conversation={
                            conversations.find(
                                x =>
                                    x.id ===
                                    selectedConversationId
                            )
                        }
                    />

                }

            />

        </PresenceProvider>

    );
}