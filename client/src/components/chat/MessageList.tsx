import { forwardRef } from "react";

import type { Message } from "../../types/message";
import MessageBubble from "./MessageBubble";

interface Props {
    messages: Message[];
    currentUserId: string;
}

const MessageList = forwardRef<HTMLDivElement, Props>(
    ({ messages, currentUserId }, ref) => {

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

                <div ref={ref} />
            </>
        );

    }
);

MessageList.displayName = "MessageList";

export default MessageList;