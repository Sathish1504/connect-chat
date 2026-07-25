import {
    createContext,
    useContext,
    useEffect,
    useMemo,
    useState
} from "react";

import { presenceService } from "./presenceService";

interface PresenceContextValue {

    onlineUsers: Set<string>;

    isUserOnline: (
        userId: string
    ) => boolean;

}

const PresenceContext =
    createContext<PresenceContextValue | undefined>(
        undefined
    );

interface Props {

    children: React.ReactNode;

}
export function PresenceProvider({
    children
}: Props) {

    const [onlineUsers, setOnlineUsers] =
        useState<Set<string>>(new Set());

    useEffect(() => {

        async function initialize() {

            const users =
                await presenceService.getOnlineUsers();

                console.log("🟢 Online Users:", users);

            setOnlineUsers(
                new Set(users)
            );

        }

        void initialize();

        presenceService.onUserOnline(userId => {

            console.log("🟢 User Online Event:", userId);

            setOnlineUsers(previous => {

                const updated = new Set(previous);

                updated.add(userId);

                return updated;

            });

        });

        presenceService.onUserOffline(userId => {

            setOnlineUsers(previous => {

                const updated = new Set(previous);

                updated.delete(userId);

                return updated;

            });

        });

        return () => {

            presenceService.offUserOnline();

            presenceService.offUserOffline();

        };

    }, []);

    // 👇 This must still exist
    const value = useMemo(() => ({

        onlineUsers,

        isUserOnline(userId: string) {

            return onlineUsers.has(userId);

        }

    }), [onlineUsers]);

    return (

        <PresenceContext.Provider value={value}>

            {children}

        </PresenceContext.Provider>

    );

}

export function usePresence() {

    const context =
        useContext(PresenceContext);

    if (!context) {

        throw new Error(
            "usePresence must be used inside PresenceProvider"
        );

    }

    return context;

}