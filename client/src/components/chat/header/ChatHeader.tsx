import {
    Phone,
    Video,
    Search,
    MoreVertical
} from "lucide-react";

import ProfileAvatar from "../../profile/ProfileAvatar";

interface Props {
    name?: string;
    profilePicture?: string | null;
    online?: boolean;
}

export default function ChatHeader({
    name = "Select Conversation",
    profilePicture,
    online = false
}: Props) {

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
                                ${
                                    online
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
                    className="
                        rounded-full
                        p-3
                        text-slate-600
                        transition-all
                        hover:bg-slate-100
                        hover:text-blue-600
                    "
                >
                    <Phone size={20} />
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