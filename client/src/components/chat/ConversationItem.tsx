import type { Conversation } from "../../types/conversation";
import { usePresence } from "../../features/presence/PresenceContext";

interface Props {
    conversation: Conversation;
    selected: boolean;
    onClick: () => void;
}

export default function ConversationItem({
    conversation,
    selected,
    onClick
}: Props) {

    const { isUserOnline } = usePresence();

    const online = isUserOnline(
        conversation.otherParticipantId
    );

    const avatarLetter =
        (conversation.displayName ?? "U")
            .charAt(0)
            .toUpperCase();

    return (

        <button
            onClick={onClick}
            className={`
                group
                relative
                mb-2
                flex
                w-full
                items-center
                gap-4
                rounded-2xl
                border
                px-4
                py-3
                text-left
                transition-all
                duration-300
                ${
                    selected
                        ? "border-blue-200 bg-gradient-to-r from-blue-50 to-indigo-50 shadow-md"
                        : "border-transparent bg-white hover:-translate-y-0.5 hover:border-slate-200 hover:bg-slate-50 hover:shadow-md"
                }
            `}
        >

            {/* Avatar */}

            <div className="relative">

                <div
                    className="
                        flex
                        h-14
                        w-14
                        items-center
                        justify-center
                        rounded-full
                        bg-gradient-to-br
                        from-blue-500
                        via-indigo-500
                        to-purple-600
                        text-lg
                        font-bold
                        text-white
                        shadow-md
                    "
                >
                    {avatarLetter}
                </div>

                <span
                    className={`
                        absolute
                        bottom-1
                        right-1
                        h-3.5
                        w-3.5
                        rounded-full
                        border-2
                        border-white
                        shadow
                        ${
                            online
                                ? "bg-green-500"
                                : "bg-slate-400"
                        }
                    `}
                />

            </div>

            {/* Content */}

            <div className="min-w-0 flex-1">

                <div className="flex items-center justify-between">

                    <h3
                        className="
                            truncate
                            text-[15px]
                            font-semibold
                            text-slate-800
                        "
                    >
                        {conversation.displayName}
                    </h3>

                    <span
                        className="
                            ml-3
                            text-xs
                            text-slate-400
                        "
                    >
                        {conversation.lastMessageAt
                            ? new Date(
                                  conversation.lastMessageAt
                              ).toLocaleTimeString([], {
                                  hour: "2-digit",
                                  minute: "2-digit"
                              })
                            : ""}
                    </span>

                </div>

                <div className="mt-1 flex items-center justify-between">

                    <p
                        className="
                            truncate
                            text-sm
                            text-slate-500
                        "
                    >
                        {conversation.lastMessage ??
                            "No messages yet"}
                    </p>

                    {/* Demo unread badge */}

                    <div
                        className="
                            ml-3
                            flex
                            h-5
                            min-w-[20px]
                            items-center
                            justify-center
                            rounded-full
                            bg-blue-600
                            px-1.5
                            text-[10px]
                            font-semibold
                            text-white
                        "
                    >
                       {/* TODO: Sprint 13 - Unread Count */} 
                    </div>

                </div>

            </div>

            {/* Active Indicator */}

            {selected && (

                <div
                    className="
                        absolute
                        left-0
                        top-3
                        bottom-3
                        w-1
                        rounded-r-full
                        bg-blue-600
                    "
                />

            )}

        </button>

    );

}