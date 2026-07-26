import { useEffect, useMemo, useState } from "react";
import { Search, X } from "lucide-react";

import { getUsers } from "../userService";
import type { User } from "../types/user";


import UserItem from "./UserItem";

interface Props {
    open: boolean;
    onClose: () => void;
    onUserSelected: (user: User) => void;
}

export default function UserPickerModal({
    open,
    onClose,
    onUserSelected
}: Props) {

    const [users, setUsers] = useState<User[]>([]);
    const [search, setSearch] = useState("");
    const [loading, setLoading] = useState(false);

    useEffect(() => {

        if (!open)
            return;

        void loadUsers();

    }, [open]);

    async function loadUsers() {

        try {

            setLoading(true);

            const result = await getUsers();

            setUsers(result);

        }
        catch (error) {

            console.error(error);

        }
        finally {

            setLoading(false);

        }

    }

    const filteredUsers = useMemo(() => {

        if (!search.trim())
            return users;

        return users.filter(user =>
            user.userName
                .toLowerCase()
                .includes(search.toLowerCase()) ||

            user.email
                .toLowerCase()
                .includes(search.toLowerCase())
        );

    }, [users, search]);

    if (!open)
        return null;

    return (

        <div className="fixed inset-0 z-50 flex items-center justify-center bg-black/40">

            <div className="w-full max-w-lg rounded-2xl bg-white shadow-2xl">

                <div className="flex items-center justify-between border-b p-5">

                    <h2 className="text-xl font-bold">
                        New Chat
                    </h2>

                    <button onClick={onClose}>
                        <X />
                    </button>

                </div>

                <div className="p-5">

                    <div className="flex items-center gap-2 rounded-xl border px-3 py-2">

                        <Search size={18} />

                        <input
                            value={search}
                            onChange={e => setSearch(e.target.value)}
                            placeholder="Search users..."
                            className="flex-1 outline-none"
                        />

                    </div>

                </div>

                <div className="max-h-96 overflow-y-auto p-3">

                    {loading && (

                        <div className="py-8 text-center text-slate-500">

                            Loading users...

                        </div>

                    )}

                    {!loading && filteredUsers.length === 0 && (

                        <div className="py-8 text-center text-slate-500">

                            No users found

                        </div>

                    )}

                    {!loading && filteredUsers.map(user => (

                        <UserItem
                            key={user.id}
                            user={user}
                            onSelect={onUserSelected}
                        />

                    ))}

                </div>

            </div>

        </div>

    );

}