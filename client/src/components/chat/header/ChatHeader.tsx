import {
    Phone,
    Video,
    Search,
    MoreVertical
} from "lucide-react";

interface Props {
    name?: string;
    online?: boolean;
}

export default function ChatHeader({
    name = "Select Conversation",
    online = false
}: Props) {

    const avatar =
        name.charAt(0).toUpperCase();

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

                <div className="relative">

                    <div
                        className="
                            flex
                            h-14
                            w-14
                            items-center
                            justify-center
                            rounded-full
                            bg-gradient-to-br
                            from-blue-500
                            via-indigo-500
                            to-purple-600
                            text-xl
                            font-bold
                            text-white
                            shadow-lg
                        "
                    >
                        {avatar}
                    </div>

                    <span
                        className={`
                            absolute
                            bottom-1
                            right-1
                            h-3.5
                            w-3.5
                            rounded-full
                            border-2
                            border-white
                            shadow
                            ${
                                online
                                    ? "bg-green-500"
                                    : "bg-slate-400"
                            }
                        `}
                    />

                </div>

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