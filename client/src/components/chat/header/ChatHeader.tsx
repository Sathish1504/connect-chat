import {
    Phone,
    Video,
    Search,
    MoreVertical
} from "lucide-react";

import ProfileAvatar from "../../profile/ProfileAvatar";
import { signalRService } from "../../../features/chat/signalrService";
import type { CallType } from "../../../features/calling/types/call";

interface Props {
    name?: string;
    profilePicture?: string | null;
    online?: boolean;
    targetUserId?: string;
    conversationId?: string;
}

export default function ChatHeader({
    name = "Select Conversation",
    profilePicture,
    online = false,
    targetUserId,
    conversationId
}: Props) {


    async function startCall(callType: CallType) {

        if (!targetUserId || !conversationId) {
            console.warn(
                "Cannot start call: missing target user or conversation"
            );
            return;
        }

        try {

            await signalRService.callUser(
                targetUserId,
                conversationId,
                callType
            );

            console.log(
                `📞 ${callType} call started`
            );

        }
        catch (error) {

            console.error(
                "Failed to start call:",
                error
            );

        }
    }
    return (

        <header
            className="
                flex
                h-20
                items-center
                justify-between
                border-b
                border-slate-200
                bg-white/90
                px-6
                backdrop-blur-md
            "
        >

            {/* Left */}

            <div className="flex items-center gap-4">

                <ProfileAvatar
                    name={name}
                    profilePicture={profilePicture}
                    online={online}
                    size="lg"
                />

                <div>

                    <h2
                        className="
                            text-lg
                            font-bold
                            text-slate-800
                        "
                    >
                        {name}
                    </h2>

                    <div
                        className="
                            mt-1
                            flex
                            items-center
                            gap-2
                        "
                    >

                        <span
                            className={`
                                h-2
                                w-2
                                rounded-full
                                ${online
                                    ? "bg-green-500"
                                    : "bg-slate-400"
                                }
                            `}
                        />

                        <p
                            className="
                                text-sm
                                text-slate-500
                            "
                        >
                            {online
                                ? "Online"
                                : "Last seen recently"}
                        </p>

                    </div>

                </div>

            </div>

            {/* Right */}

            <div className="flex items-center gap-2">

                <button
                    className="
                        rounded-full
                        p-3
                        text-slate-600
                        transition-all
                        hover:bg-slate-100
                        hover:text-blue-600
                    "
                >
                    <Search size={20} />
                </button>

                <button
                    type="button"
                    onClick={() => void startCall("audio")}
                    disabled={!targetUserId || !conversationId}
                    className="
                        rounded-full
                        p-3
                        text-slate-600
                        transition-all
                        hover:bg-slate-100
                        hover:text-blue-600
                        disabled:cursor-not-allowed
                        disabled:opacity-40
                    "
                >
                    <Phone size={20} />
                </button>

                <button
                    type="button"
                    onClick={() => void startCall("video")}
                    disabled={!targetUserId || !conversationId}
                    className="
                        rounded-full
                        p-3
                        text-slate-600
                        transition-all
                        hover:bg-slate-100
                        hover:text-blue-600
                        disabled:cursor-not-allowed     
                        disabled:opacity-40
                    "
                >
                    <Video size={20} />
                </button>

                <button
                    className="
                        rounded-full
                        p-3
                        text-slate-600
                        transition-all
                        hover:bg-slate-100
                        hover:text-blue-600
                    "
                >
                    <MoreVertical size={20} />
                </button>

            </div>

        </header>

    );
}