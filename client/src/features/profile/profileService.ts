import { identityApi } from "../../api/identityApi";

export interface ProfilePictureResponse {
    profilePictureUrl: string;
}

export interface ProfileResponse {
    id: string;
    userName: string;
    email: string;
    profilePicture: string | null;
    emailConfirmed: boolean;
    isOnline: boolean;
    isActive: boolean;
    createdAt: string;
    lastSeenAt: string | null;
}

export async function uploadProfilePicture(
    file: File
): Promise<ProfilePictureResponse> {

    const formData = new FormData();

    formData.append("file", file);

    const response =
        await identityApi.post<ProfilePictureResponse>(
            "/Users/profile-picture",
            formData
        );

    return response.data;
}

export async function deleteProfilePicture(): Promise<void> {

    await identityApi.delete(
        "/Users/profile-picture"
    );
}

export async function getProfile(): Promise<ProfileResponse> {

    const response =
        await identityApi.get<ProfileResponse>(
            "/Users/profile"
        );

    return response.data;
}