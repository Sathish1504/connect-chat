export interface User {
    id: string;
    userName: string;
    email: string;
    profilePicture?: string | null;
    isOnline: boolean;
}