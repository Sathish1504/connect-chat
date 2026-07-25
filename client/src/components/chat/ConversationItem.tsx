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

    return (

        <div
            onClick={onClick}
            style={{
                padding: "14px",
                cursor: "pointer",
                borderBottom: "1px solid #eee",
                background: selected ? "#f5f5f5" : "white"
            }}
        >

            <div
                style={{
                    display: "flex",
                    alignItems: "center",
                    gap: 8
                }}
            >

                <span
                    style={{
                        width: 10,
                        height: 10,
                        borderRadius: "50%",
                        backgroundColor: online
                            ? "#22c55e"
                            : "#9ca3af",
                        flexShrink: 0
                    }}
                />

                <div
                    style={{
                        fontWeight: 600
                    }}
                >
                    {conversation.name}
                </div>

            </div>

            <div
                style={{
                    color: "#666",
                    fontSize: 14,
                    marginTop: 4,
                    marginLeft: 18
                }}
            >
                {conversation.lastMessage}
            </div>

        </div>

    );

}