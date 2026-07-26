import { useEffect, useState } from "react";
import { Search, X } from "lucide-react";

import { getUsers } from "../../features/users/userService";

import type { User } from "../../features/users/types/user"
import UserItem from "./UserItem";

import {
    createConversation
} from "../../features/chat/conversationService";

interface Props {
    open: boolean;
    onClose: () => void;
    onConversationCreated: (conversationId: string) => void;
}

export default function UserPickerModal({
    open,
    onClose,
    onConversationCreated
}: Props) {

    const [users, setUsers] = useState<User[]>([]);
    const [search, setSearch] = useState("");

    useEffect(() => {

        if (!open) return;

        loadUsers();

    }, [open]);

    async function loadUsers() {

        const result = await getUsers();

        setUsers(result);

    }
    async function handleUserSelected(user: User) {

    try {

        const result = await createConversation({

            type: 1,
            name: "",
            participantIds: [user.id]

        });

        onConversationCreated(result.conversationId);

        onClose();

    } catch (error) {

        console.error(error);

    }

}

    if (!open)
        return null;

    const filteredUsers = users.filter(x =>
        x.userName
            .toLowerCase()
            .includes(search.toLowerCase())
    );

    return (

        <div
            className="
                fixed
                inset-0
                z-50
                flex
                items-center
                justify-center
                bg-black/40
            "
        >

            <div
                className="
                    w-full
                    max-w-lg
                    rounded-2xl
                    bg-white
                    shadow-2xl
                "
            >

                <div
                    className="
                        flex
                        items-center
                        justify-between
                        border-b
                        p-5
                    "
                >

                    <h2 className="text-xl font-bold">
                        New Chat
                    </h2>

                    <button onClick={onClose}>
                        <X />
                    </button>

                </div>

                <div className="p-5">

                    <div
                        className="
                            flex
                            items-center
                            gap-2
                            rounded-xl
                            border
                            px-3
                            py-2
                        "
                    >

                        <Search size={18} />

                        <input
                            value={search}
                            onChange={e =>
                                setSearch(e.target.value)
                            }
                            placeholder="Search users..."
                            className="
                                flex-1
                                outline-none
                            "
                        />

                    </div>

                </div>

                <div
                    className="
                        max-h-96
                        overflow-y-auto
                        p-3
                    "
                >

                    {filteredUsers.map(user => (

                        <UserItem
                            key={user.id}
                            user={user}
                            onSelect={handleUserSelected}
                        />

                    ))}

                </div>

            </div>

        </div>

    );

}