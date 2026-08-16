import { IDENTITY_API_BASE_URL } from "../../api/identityApi";

export function getProfileImageUrl(
    profilePicture?: string | null
): string | undefined {

    if (!profilePicture) {
        return undefined;
    }

    if (
        profilePicture.startsWith("http://") ||
        profilePicture.startsWith("https://")
    ) {
        return profilePicture;
    }

    return `${IDENTITY_API_BASE_URL}${profilePicture}`;
}