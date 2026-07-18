import type { Message } from "../../types/message";
import MessageBubble from "./MessageBubble";

interface Props {
    messages: Message[];
    currentUserId: string;
}

export default function MessageList({
    messages,
    currentUserId
}: Props) {

    return (
        <>
            {messages.map(message => (
                <MessageBubble
                    key={message.id}
                    message={message}
                    isOwnMessage={
                        message.senderId === currentUserId
                    }
                />
            ))}
        </>
    );
}