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
                <span className="italic text-[11px] text-slate-300">
                    Sending...
                </span>
            );
        }

        switch (message.status) {

            case 1:
                return (
                    <span className="text-slate-200">
                        ✓
                    </span>
                );

            case 2:
                return (
                    <span className="text-slate-200">
                        ✓✓
                    </span>
                );

            case 3:
                return (
                    <span className="font-semibold text-sky-500">
                        ✓✓
                    </span>
                );

            default:
                return null;
        }

    }

    return (

        <div
            className={`
                mb-4
                flex
                animate-[fadeIn_.18s_ease]
                ${isOwnMessage
                    ? "justify-end"
                    : "justify-start"}
            `}
        >

            <div
                className={`
                    max-w-[72%]
                    rounded-3xl
                    px-4
                    py-3
                    shadow-md
                    transition-all
                    duration-200
                    hover:shadow-lg
                    ${isOwnMessage
                        ? "rounded-br-md bg-blue-600 text-white"
                        : "rounded-bl-md bg-slate-700 text-white"}
                    ${message.isPending
                        ? "opacity-60"
                        : "opacity-100"}
                `}
            >

                <div
                    className="
                        break-words
                        whitespace-pre-wrap
                        text-[15px]
                        leading-6
                    "
                >
                    {message.content}
                </div>

                <div
                    className="
                        mt-2
                        flex
                        items-center
                        justify-end
                        gap-2
                        text-[11px]
                        text-white/70
                    "
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