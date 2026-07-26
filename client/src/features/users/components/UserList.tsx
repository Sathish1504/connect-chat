import type { User } from "../types/user";
import UserItem from "./UserItem";

interface Props {
    users: User[];
    onSelect(user: User): void;
}

export default function UserList({ users, onSelect }: Props) {
    return (
        <>
            {users.map(user => (
                <UserItem
                    key={user.id}
                    user={user}
                    onSelect={onSelect}
                />
            ))}
        </>
    );
}