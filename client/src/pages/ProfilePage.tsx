import { useEffect, useState } from "react";
import { ArrowLeft, Trash2 } from "lucide-react";
import { useNavigate } from "react-router-dom";

import ProfilePictureUploader from "../components/profile/ProfilePictureUploader";

import {
    deleteProfilePicture,
    getProfile,
    type ProfileResponse
} from "../features/profile/profileService";

export default function ProfilePage() {

    const navigate = useNavigate();

    const [profile, setProfile] =
        useState<ProfileResponse | null>(null);

    const [loading, setLoading] =
        useState(true);

    const [deleting, setDeleting] =
        useState(false);

    const [error, setError] =
        useState<string | null>(null);

    useEffect(() => {

        void loadProfile();

    }, []);

    async function loadProfile() {

        try {

            setLoading(true);
            setError(null);

            const result = await getProfile();

            setProfile(result);

        }
        catch (error) {

            console.error(
                "Failed to load profile:",
                error
            );

            setError(
                "Failed to load your profile."
            );

        }
        finally {

            setLoading(false);

        }
    }

    async function handleDeletePicture() {

        if (!profile?.profilePicture) {
            return;
        }

        try {

            setDeleting(true);
            setError(null);

            await deleteProfilePicture();

            setProfile(current =>
                current
                    ? {
                        ...current,
                        profilePicture: null
                    }
                    : current
            );

        }
        catch (error) {

            console.error(
                "Failed to delete profile picture:",
                error
            );

            setError(
                "Failed to remove profile picture."
            );

        }
        finally {

            setDeleting(false);

        }
    }

    function handleUploaded(
        profilePicture: string
    ) {

        setProfile(current =>
            current
                ? {
                    ...current,
                    profilePicture
                }
                : current
        );
    }

    if (loading) {

        return (
            <div className="
                flex
                min-h-screen
                items-center
                justify-center
                bg-slate-100
            ">
                <p className="text-slate-500">
                    Loading profile...
                </p>
            </div>
        );

    }

    if (!profile) {

        return (
            <div className="
                flex
                min-h-screen
                flex-col
                items-center
                justify-center
                gap-4
                bg-slate-100
            ">
                <p className="text-red-500">
                    {error ?? "Profile not found."}
                </p>

                <button
                    type="button"
                    onClick={() => navigate("/dashboard")}
                    className="
                        rounded-xl
                        bg-blue-600
                        px-4
                        py-2
                        text-white
                        hover:bg-blue-700
                    "
                >
                    Back to Dashboard
                </button>
            </div>
        );

    }

    return (

        <div className="
            min-h-screen
            bg-gradient-to-br
            from-slate-100
            via-white
            to-blue-50
            p-6
        ">

            <div className="
                mx-auto
                max-w-3xl
            ">

                {/* Header */}

                <div className="
                    mb-6
                    flex
                    items-center
                    gap-4
                ">

                    <button
                        type="button"
                        onClick={() => navigate("/dashboard")}
                        className="
                            rounded-full
                            p-2
                            text-slate-600
                            hover:bg-white
                            hover:text-blue-600
                        "
                        aria-label="Back to dashboard"
                    >
                        <ArrowLeft size={22} />
                    </button>

                    <div>
                        <h1 className="
                            text-2xl
                            font-bold
                            text-slate-800
                        ">
                            My Profile
                        </h1>

                        <p className="
                            text-sm
                            text-slate-500
                        ">
                            Manage your profile picture
                        </p>
                    </div>

                </div>

                {/* Profile Card */}

                <div className="
                    rounded-3xl
                    border
                    border-slate-200
                    bg-white
                    p-8
                    shadow-xl
                ">

                    <div className="
                        flex
                        flex-col
                        items-center
                    ">

                        <ProfilePictureUploader
                            name={profile.userName}
                            profilePicture={
                                profile.profilePicture
                            }
                            onUploaded={handleUploaded}
                        />

                        <h2 className="
                            mt-6
                            text-xl
                            font-bold
                            text-slate-800
                        ">
                            {profile.userName}
                        </h2>

                        <p className="
                            mt-1
                            text-sm
                            text-slate-500
                        ">
                            {profile.email}
                        </p>

                    </div>

                    {/* Remove Picture */}

                    {profile.profilePicture && (

                        <div className="
                            mt-8
                            border-t
                            border-slate-200
                            pt-6
                        ">

                            <button
                                type="button"
                                onClick={handleDeletePicture}
                                disabled={deleting}
                                className="
                                    flex
                                    w-full
                                    items-center
                                    justify-center
                                    gap-2
                                    rounded-xl
                                    border
                                    border-red-200
                                    px-4
                                    py-3
                                    text-sm
                                    font-medium
                                    text-red-600
                                    transition
                                    hover:bg-red-50
                                    disabled:cursor-not-allowed
                                    disabled:opacity-60
                                "
                            >

                                <Trash2 size={17} />

                                {deleting
                                    ? "Removing..."
                                    : "Remove Profile Picture"}

                            </button>

                        </div>

                    )}

                    {error && (

                        <p className="
                            mt-4
                            text-center
                            text-sm
                            text-red-500
                        ">
                            {error}
                        </p>

                    )}

                </div>

            </div>

        </div>

    );
}