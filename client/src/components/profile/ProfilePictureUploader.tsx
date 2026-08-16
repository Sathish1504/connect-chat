import { useRef, useState } from "react";
import { Camera, Upload } from "lucide-react";

import ProfileAvatar from "./ProfileAvatar";

import {
    uploadProfilePicture
} from "../../features/profile/profileService";

interface Props {
    name: string;
    profilePicture?: string | null;
    onUploaded: (profilePicture: string) => void;
}

const MAX_FILE_SIZE = 5 * 1024 * 1024;

const ALLOWED_TYPES = [
    "image/jpeg",
    "image/png",
    "image/webp"
];

export default function ProfilePictureUploader({
    name,
    profilePicture,
    onUploaded
}: Props) {

    const fileInputRef = useRef<HTMLInputElement>(null);

    const [preview, setPreview] =
        useState<string | null>(null);

    const [uploading, setUploading] =
        useState(false);

    const [error, setError] =
        useState<string | null>(null);

    function handleFileSelected(
        event: React.ChangeEvent<HTMLInputElement>
    ) {

        const file = event.target.files?.[0];

        if (!file) {
            return;
        }

        setError(null);

        if (!ALLOWED_TYPES.includes(file.type)) {

            setError(
                "Please select a JPG, PNG, or WebP image."
            );

            return;
        }

        if (file.size > MAX_FILE_SIZE) {

            setError(
                "Profile picture must be smaller than 5 MB."
            );

            return;
        }

        const previewUrl =
            URL.createObjectURL(file);

        setPreview(previewUrl);

        void handleUpload(file);
    }

    async function handleUpload(file: File) {

        try {

            setUploading(true);

            setError(null);

            const result =
                await uploadProfilePicture(file);

            onUploaded(
                result.profilePictureUrl
            );

        }
        catch (error) {

            console.error(
                "Profile picture upload failed:",
                error
            );

            setError(
                "Failed to upload profile picture."
            );

            setPreview(null);
        }
        finally {

            setUploading(false);

        }
    }

    const displayedPicture =
        preview ?? profilePicture;

    return (

        <div className="flex flex-col items-center">

            <div className="relative">

                <ProfileAvatar
                    name={name}
                    profilePicture={displayedPicture}
                    size="xl"
                />

                <button
                    type="button"
                    onClick={() =>
                        fileInputRef.current?.click()
                    }
                    disabled={uploading}
                    className="
                        absolute
                        bottom-0
                        right-0
                        flex
                        h-10
                        w-10
                        items-center
                        justify-center
                        rounded-full
                        border-4
                        border-white
                        bg-blue-600
                        text-white
                        shadow-lg
                        transition
                        hover:bg-blue-700
                        disabled:cursor-not-allowed
                        disabled:opacity-60
                    "
                    aria-label="Change profile picture"
                >
                    <Camera size={18} />
                </button>

            </div>

            <input
                ref={fileInputRef}
                type="file"
                accept="image/jpeg,image/png,image/webp"
                className="hidden"
                onChange={handleFileSelected}
            />

            <button
                type="button"
                onClick={() =>
                    fileInputRef.current?.click()
                }
                disabled={uploading}
                className="
                    mt-4
                    flex
                    items-center
                    gap-2
                    rounded-xl
                    bg-blue-600
                    px-4
                    py-2
                    text-sm
                    font-medium
                    text-white
                    transition
                    hover:bg-blue-700
                    disabled:cursor-not-allowed
                    disabled:opacity-60
                "
            >
                <Upload size={16} />

                {uploading
                    ? "Uploading..."
                    : "Change Picture"}
            </button>

            {error && (
                <p
                    className="
                        mt-3
                        text-center
                        text-sm
                        text-red-500
                    "
                >
                    {error}
                </p>
            )}

        </div>

    );
}