import type { Message } from "../../types/message";

interface Props {
    message: Message;
    isOwnMessage: boolean;
}

export default function MessageBubble({
    message,
    isOwnMessage
}: Props) {

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
                    color: "white"
                }}
            >

                <div>

                    {message.content}

                </div>

                <div
                    style={{
                        fontSize: 11,
                        opacity: 0.7,
                        marginTop: 6
                    }}
                >

                    {new Date(message.createdAt)
                        .toLocaleTimeString([], {
                            hour: "2-digit",
                            minute: "2-digit"
                        })}

                </div>

            </div>

        </div>

    );

}