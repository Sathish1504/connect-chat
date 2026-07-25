import type { Message } from "../../types/message";

interface Props {
    message: Message;
    isOwnMessage: boolean;
}

export default function MessageBubble({
    message,
    isOwnMessage
}: Props) {

    function renderStatus() {

        if (!isOwnMessage)
            return null;

        if (message.isPending) {
            return (
                <span
                    style={{
                        fontStyle: "italic"
                    }}
                >
                    Sending...
                </span>
            );
        }

        switch (message.status) {

            case 1:
                return <span>✓</span>;

            case 2:
                return <span>✓✓</span>;

            case 3:
                return (
                    <span
                        style={{
                            color: "#fcfcfc",
                            fontWeight: 600
                        }}
                    >
                        ✓✓
                    </span>
                );

            default:
                return null;
        }

    }

    return (

        <div
            style={{
                display: "flex",
                justifyContent: isOwnMessage
                    ? "flex-end"
                    : "flex-start",
                marginBottom: 12
            }}
        >

            <div
                style={{
                    maxWidth: "70%",
                    padding: "12px 16px",
                    borderRadius: 16,
                    backgroundColor: isOwnMessage
                        ? "#2563eb"
                        : "#2d3748",
                    color: "white",
                    opacity: message.isPending ? 0.6 : 1,
                    transition: "opacity 0.2s ease"
                }}
            >

                <div>

                    {message.content}

                </div>

                <div
                    style={{
                        display: "flex",
                        justifyContent: "space-between",
                        alignItems: "center",
                        gap: 8,
                        fontSize: 11,
                        opacity: 0.7,
                        marginTop: 6
                    }}
                >

                    <span>
                        {new Date(message.createdAt)
                            .toLocaleTimeString([], {
                                hour: "2-digit",
                                minute: "2-digit"
                            })}
                    </span>

                    {renderStatus()}

                </div>

            </div>

        </div>

    );

}