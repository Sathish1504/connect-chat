import type { User } from "../../features/users/types/user";

import ProfileAvatar from "../profile/ProfileAvatar";

interface Props {
    user: User;
    onSelect: (user: User) => void;
}

export default function UserItem({
    user,
    onSelect
}: Props) {

    return (
        <button
            onClick={() => onSelect(user)}
            className="
                flex
                w-full
                items-center
                gap-4
                rounded-xl
                p-4
                text-left
                transition
                hover:bg-slate-100
            "
        >
            <ProfileAvatar
                name={user.userName}
                profilePicture={user.profilePicture}
                online={user.isOnline}
                size="md"
            />

            <div className="min-w-0 flex-1">

                <span
                    className="
                        block
                        truncate
                        font-semibold
                        text-slate-800
                    "
                >
                    {user.userName}
                </span>

                <span
                    className="
                        block
                        truncate
                        text-sm
                        text-slate-500
                    "
                >
                    {user.email}
                </span>

            </div>
        </button>
    );
}