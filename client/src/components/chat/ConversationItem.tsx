import type { Conversation } from "../../types/conversation";

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
                    fontWeight: 600
                }}
            >
                {conversation.name}
            </div>

            <div
                style={{
                    color: "#666",
                    fontSize: 14,
                    marginTop: 4
                }}
            >
                {conversation.lastMessage}
            </div>
        </div>
    );
}