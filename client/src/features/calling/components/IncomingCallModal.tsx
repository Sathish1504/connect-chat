import type { IncomingCall } from "../types/call";

interface Props {
    call: IncomingCall;
    callerName?: string;
    onAccept: () => void;
    onReject: () => void;
}

export default function IncomingCallModal({
    call,
    callerName = "Unknown",
    onAccept,
    onReject
}: Props) {

    const isVideo = call.callType === "video";

    return (
        <div
            className="
                fixed
                inset-0
                z-[100]
                flex
                items-center
                justify-center
                bg-black/50
                backdrop-blur-sm
            "
        >
            <div
                className="
                    w-full
                    max-w-sm
                    rounded-3xl
                    bg-white
                    p-8
                    text-center
                    shadow-2xl
                "
            >

                <div
                    className="
                        mx-auto
                        flex
                        h-24
                        w-24
                        items-center
                        justify-center
                        rounded-full
                        bg-gradient-to-br
                        from-blue-500
                        via-indigo-500
                        to-purple-600
                        text-3xl
                        font-bold
                        text-white
                    "
                >
                    {callerName
                        .charAt(0)
                        .toUpperCase()}
                </div>

                <h2
                    className="
                        mt-5
                        text-2xl
                        font-bold
                        text-slate-800
                    "
                >
                    {callerName}
                </h2>

                <p className="mt-2 text-slate-500">
                    Incoming {isVideo ? "video" : "audio"} call
                </p>

                <div className="mt-8 flex justify-center gap-6">

                    <button
                        type="button"
                        onClick={onReject}
                        className="
                            flex
                            h-14
                            w-14
                            items-center
                            justify-center
                            rounded-full
                            bg-red-500
                            text-white
                            shadow-lg
                            transition
                            hover:bg-red-600
                        "
                        aria-label="Reject call"
                    >
                        ❌
                    </button>

                    <button
                        type="button"
                        onClick={onAccept}
                        className="
                            flex
                            h-14
                            w-14
                            items-center
                            justify-center
                            rounded-full
                            bg-green-500
                            text-white
                            shadow-lg
                            transition
                            hover:bg-green-600
                        "
                        aria-label="Accept call"
                    >
                        📞
                    </button>

                </div>

            </div>
        </div>
    );
}