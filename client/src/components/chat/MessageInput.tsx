import { useEffect, useRef, useState } from "react";

interface Props {
    onSend: (content: string) => Promise<void>;
    onTypingStart?: () => void;
    onTypingStop?: () => void;
}

export default function MessageInput({
    onSend,
    onTypingStart,
    onTypingStop
}: Props) {

    const [message, setMessage] = useState("");

    const typingStarted = useRef(false);

    const stopTypingTimer = useRef<number | null>(null);

    function resetStopTypingTimer() {

        if (stopTypingTimer.current !== null) {
            window.clearTimeout(stopTypingTimer.current);
        }

        stopTypingTimer.current = window.setTimeout(() => {

            typingStarted.current = false;

            onTypingStop?.();

        }, 3000);

    }

    function handleTyping(
        value: string
    ) {

        setMessage(value);

        if (!typingStarted.current && value.trim()) {

            typingStarted.current = true;

            onTypingStart?.();

        }

        resetStopTypingTimer();

    }

    async function handleSend() {

        if (!message.trim())
            return;

        await onSend(message);

        setMessage("");

        if (typingStarted.current) {

            typingStarted.current = false;

            onTypingStop?.();

        }

        if (stopTypingTimer.current) {

            window.clearTimeout(stopTypingTimer.current);

        }

    }

    async function handleKeyDown(
        e: React.KeyboardEvent<HTMLInputElement>
    ) {

        if (e.key === "Enter") {

            await handleSend();

        }

    }

    useEffect(() => {

        return () => {

            if (stopTypingTimer.current) {

                window.clearTimeout(stopTypingTimer.current);

            }

            if (typingStarted.current) {

                onTypingStop?.();

            }

        };

    }, [onTypingStop]);

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
                onChange={e => handleTyping(e.target.value)}
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