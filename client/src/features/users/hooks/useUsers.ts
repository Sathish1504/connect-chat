import { chatApi } from "../../../api/chatApi";
import type { User } from "../types/user";
import { useEffect, useState } from "react";

export async function getUsers() {
    const response = await chatApi.get<User[]>("/users");

    return response.data;
}

export async function searchUsers(query: string) {
    const response = await chatApi.get<User[]>("/users/search", {
        params: { query }
    });

    return response.data;
}

export function useUsers() {
    const [users, setUsers] = useState<User[]>([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        load();
    }, []);

    async function load() {
        try {
            const result = await getUsers();
            setUsers(result);
        } finally {
            setLoading(false);
        }
    }

    return {
        users,
        loading,
        reload: load
    };
}