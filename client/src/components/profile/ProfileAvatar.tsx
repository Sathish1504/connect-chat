import { useEffect, useState } from "react";
import { getProfileImageUrl } from "../../features/profile/profileImage";

interface ProfileAvatarProps {
    name?: string;
    profilePicture?: string | null;
    online?: boolean;
    size?: "sm" | "md" | "lg" | "xl";
    className?: string;
}

const sizeClasses = {
    sm: "h-8 w-8 text-xs",
    md: "h-10 w-10 text-sm",
    lg: "h-14 w-14 text-xl",
    xl: "h-24 w-24 text-3xl"
};

const statusSizeClasses = {
    sm: "h-2.5 w-2.5 border",
    md: "h-3 w-3 border-2",
    lg: "h-3.5 w-3.5 border-2",
    xl: "h-5 w-5 border-2"
};

export default function ProfileAvatar({
    name = "User",
    profilePicture,
    online = false,
    size = "md",
    className = ""
}: ProfileAvatarProps) {

    const [imageError, setImageError] = useState(false);

    const avatar =
        name.trim().charAt(0).toUpperCase() || "U";

    const imageUrl =
        getProfileImageUrl(profilePicture);

    useEffect(() => {
    setImageError(false);
}, [imageUrl]);

    const showImage =
        Boolean(imageUrl) && !imageError;

    return (
        <div
            className={`
                relative
                shrink-0
                ${className}
            `}
        >
            <div
                className={`
                    ${sizeClasses[size]}
                    flex
                    items-center
                    justify-center
                    overflow-hidden
                    rounded-full
                    bg-gradient-to-br
                    from-blue-500
                    via-indigo-500
                    to-purple-600
                    font-bold
                    text-white
                    shadow
                `}
            >
                {showImage ? (
                    <img
                        src={imageUrl}
                        alt={`${name}'s profile`}
                        className="
                            h-full
                            w-full
                            object-cover
                        "
                        onError={() => setImageError(true)}
                    />
                ) : (
                    avatar
                )}
            </div>

            {online && (
                <span
                    className={`
                        ${statusSizeClasses[size]}
                        absolute
                        bottom-0
                        right-0
                        rounded-full
                        border-white
                        bg-green-500
                        shadow
                    `}
                    aria-label="Online"
                />
            )}
        </div>
    );
}