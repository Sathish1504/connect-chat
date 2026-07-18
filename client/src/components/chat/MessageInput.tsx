import { useState } from "react";

interface Props {
    onSend: (content: string) => Promise<void>;
}

export default function MessageInput({
    onSend
}: Props) {

    const [message, setMessage] =
        useState("");

    async function handleSend() {

        if (!message.trim())
            return;

        await onSend(message);

        setMessage("");
    }

    async function handleKeyDown(
        e: React.KeyboardEvent<HTMLInputElement>
    ) {

        if (e.key === "Enter") {

            await handleSend();

        }

    }

    return (

        <div
            style={{
                display: "flex",
                gap: 10,
                padding: 16,
                borderTop: "1px solid #ddd"
            }}
        >

            <input
                value={message}
                onChange={e =>
                    setMessage(e.target.value)
                }
                onKeyDown={handleKeyDown}
                placeholder="Type a message..."
                style={{
                    flex: 1,
                    padding: 10
                }}
            />

            <button
                onClick={handleSend}
            >
                Send
            </button>

        </div>

    );

}