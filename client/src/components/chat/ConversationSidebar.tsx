import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { Search, MessageCircle, Plus } from "lucide-react";

import type { Conversation } from "../../types/conversation";

// import { getConversations } from "../../features/chat/conversationService";

import ConversationItem from "./ConversationItem";

import UserPickerModal from "../users/UserPickerModal";

import ProfileAvatar from "../profile/ProfileAvatar";


interface Props {
    conversations: (Conversation & {
        displayName: string;
    })[];

    selectedConversationId?: string;

    onConversationSelected: (
        conversationId: string
    ) => void;

    onRefresh: () => Promise<void>;

    profilePicture?: string | null;

    userName?: string;
}

export default function ConversationSidebar({

    conversations,
    selectedConversationId,
    onConversationSelected,
    onRefresh,
    profilePicture,
    userName

}: Props) {

    const navigate = useNavigate();

    const [showUserPicker, setShowUserPicker] =
        useState(false);


    return (

        <div className="flex h-full flex-col bg-gradient-to-b from-slate-50 via-white to-blue-50">

            {/* Header */}

            <div
                className="
        border-b
        border-slate-200
        bg-white
        px-6
        py-5
    "
            >

                <div className="flex items-center gap-4">

                    <div className="flex h-14 w-14 items-center justify-center rounded-2xl bg-blue-600 text-white shadow-lg">
                        <MessageCircle size={28} />
                    </div>

                    <div className="flex flex-col">

                        <h1
                            className="
        text-2xl
        font-extrabold
        bg-gradient-to-r
        from-sky-500
        via-blue-600
        to-indigo-700
        bg-clip-text
        text-indigo-600
    "
                        >
                            ConnectChat
                        </h1>

                        <p className="text-sm text-slate-500">
                            Enterprise Messenger
                        </p>

                    </div>

                </div>

            </div>

            {/* Search */}

            <div className="p-5">

                <div
                    className="
                flex
                items-center
                gap-3
                rounded-2xl
                bg-white
                px-4
                py-3
                shadow-md
                ring-1
                ring-slate-200
                transition-all
                duration-300
                focus-within:ring-2
                focus-within:ring-blue-500
                focus-within:shadow-xl
            "
                >

                    <Search
                        size={18}
                        className="text-blue-500"
                    />

                    <input
                        placeholder="Search conversations..."
                        className="
                    flex-1
                    bg-transparent
                    outline-none
                    text-slate-700
                    placeholder:text-slate-400
                "
                    />

                </div>

            </div>

            {/* Title */}

            {/* Conversations Header */}

            <div className="flex items-center justify-between px-6 pb-3">

                <h2
                    className="
            text-xs
            font-bold
            uppercase
            tracking-[0.25em]
            text-blue-500
        "
                >
                    Conversations
                </h2>

                <button
                    onClick={() => setShowUserPicker(true)}
                    className="
            flex
            items-center
            gap-2
            rounded-xl
            bg-blue-600
            px-3
            py-2
            text-sm
            font-medium
            text-white
            transition
            hover:bg-blue-700
        "
                >
                    <Plus size={16} />

                    New Chat
                </button>

            </div>

            {/* List */}

            <div
                className="
            flex-1
            overflow-y-auto
            px-3
            pb-4
            space-y-3
        "
            >

                {conversations.length === 0 ? (

                    <div
                        className="
                    mt-20
                    flex
                    flex-col
                    items-center
                    text-slate-400
                "
                    >

                        <div
                            className="
                        flex
                        h-20
                        w-20
                        items-center
                        justify-center
                        rounded-full
                        bg-gradient-to-br
                        from-blue-500
                        to-purple-600
                        text-white
                        shadow-xl
                    "
                        >
                            <MessageCircle size={36} />
                        </div>

                        <p className="mt-5 text-lg font-medium">
                            No conversations
                        </p>

                        <p className="text-sm">
                            Start chatting with your team.
                        </p>

                    </div>

                ) : (

                    conversations.map(conversation => (

                        <ConversationItem
                            key={conversation.id}
                            conversation={conversation}
                            selected={
                                conversation.id ===
                                selectedConversationId
                            }
                            onClick={() =>
                                onConversationSelected(conversation.id)
                            }
                        />

                    ))

                )}

            </div>

            <button
                type="button"
                onClick={() => navigate("/profile")}
                className="
        w-full
        border-t
        border-slate-200
        bg-white
        px-5
        py-4
        text-left
        transition
        hover:bg-slate-50
    "
            >
                <div className="flex items-center gap-3">

                    <ProfileAvatar
                        name={userName}
                        profilePicture={profilePicture}
                        size="md"
                    />

                    <div className="min-w-0 flex-1">

                        <p
                            className="
                    truncate
                    text-sm
                    font-semibold
                    text-slate-800
                "
                        >
                            {userName || "User"}
                        </p>

                        <p
                            className="
                    text-xs
                    text-slate-500
                "
                        >
                            My Profile
                        </p>

                    </div>

                </div>
            </button>

            <UserPickerModal
                open={showUserPicker}
                onClose={() => setShowUserPicker(false)}
                onConversationCreated={async (conversationId) => {

                    await onRefresh();

                    onConversationSelected(conversationId);

                    setShowUserPicker(false);

                }}
            />
        </div>

    );

}