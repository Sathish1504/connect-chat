import type { User } from "../types/user";

interface Props {
    user: User;
    onSelect(user: User): void;
}

export default function UserItem({ user, onSelect }: Props) {
    return (
        <div
            onClick={() => onSelect(user)}
            className="flex items-center justify-between p-3 hover:bg-gray-100 cursor-pointer"
        >
            <div>
                <div className="font-medium">
                    {user.userName}
                </div>

                <div className="text-xs text-gray-500">
                    {user.email}
                </div>
            </div>

            <div
                className={`w-3 h-3 rounded-full ${
                    user.isOnline ? "bg-green-500" : "bg-gray-400"
                }`}
            />
        </div>
    );
}