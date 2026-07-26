import { useEffect, useRef, useState } from "react";

import {
    SendHorizontal,
    Smile,
    Paperclip
} from "lucide-react";

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
        className="
            border-t
            border-slate-200
            bg-white/90
            backdrop-blur-md
            px-6
            py-5
        "
    >

        <div
            className="
                mx-auto
                flex
                max-w-4xl
                items-center
                gap-3
                rounded-full
                border
                border-slate-200
                bg-white
                px-4
                py-3
                shadow-lg
                transition-all
                duration-300
                focus-within:border-blue-500
                focus-within:ring-4
                focus-within:ring-blue-100
            "
        >

            {/* Emoji */}

            <button
                className="
                    rounded-full
                    p-2
                    text-slate-500
                    transition
                    hover:bg-slate-100
                    hover:text-yellow-500
                "
            >
                <Smile size={22} />
            </button>

            {/* Attachment */}

            <button
                className="
                    rounded-full
                    p-2
                    text-slate-500
                    transition
                    hover:bg-slate-100
                    hover:text-blue-600
                "
            >
                <Paperclip size={22} />
            </button>

            {/* Input */}

            <input
                value={message}
                onChange={e => handleTyping(e.target.value)}
                onKeyDown={handleKeyDown}
                placeholder="Write a message..."
                className="
                    flex-1
                    bg-transparent
                    text-[15px]
                    text-slate-700
                    placeholder:text-slate-400
                    outline-none
                "
            />

            {/* Send */}

            <button
                onClick={handleSend}
                disabled={!message.trim()}
                className={`
                    flex
                    h-12
                    w-12
                    items-center
                    justify-center
                    rounded-full
                    transition-all
                    duration-300
                    ${
                        message.trim()
                            ? "bg-gradient-to-r from-blue-500 to-indigo-600 text-white shadow-lg hover:scale-110 hover:shadow-xl"
                            : "bg-slate-200 text-slate-400 cursor-not-allowed"
                    }
                `}
            >
                <SendHorizontal
                    size={20}
                    className={
                        message.trim()
                            ? "translate-x-[1px]"
                            : ""
                    }
                />
            </button>

        </div>

    </div>

);
}