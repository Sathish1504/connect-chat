import type { User } from "../../features/users/types/user"

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
                justify-between
                rounded-xl
                p-4
                transition
                hover:bg-slate-100
            "
        >

            <div className="flex flex-col items-start">

                <span className="font-semibold">
                    {user.userName}
                </span>

                <span className="text-sm text-slate-500">
                    {user.email}
                </span>

            </div>

            <div
                className={`h-3 w-3 rounded-full ${
                    user.isOnline
                        ? "bg-green-500"
                        : "bg-slate-400"
                }`}
            />

        </button>

    );

}