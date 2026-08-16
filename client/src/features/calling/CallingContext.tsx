import {
    createContext,
    useContext,
    useEffect,
    useMemo,
    useState,
    type ReactNode
} from "react";

import { useAuth } from "../../auth/AuthContext";
import IncomingCallModal from "./components/IncomingCallModal";

import { signalRService } from "../chat/signalrService";

import type { IncomingCall } from "./types/call";

interface CallingContextValue {
    incomingCall: IncomingCall | null;
    clearIncomingCall: () => void;
}

const CallingContext =
    createContext<CallingContextValue | undefined>(
        undefined
    );

interface Props {
    children: ReactNode;
}

export function CallingProvider({
    children
}: Props) {

    const { isAuthenticated } = useAuth();

    const [incomingCall, setIncomingCall] =
        useState<IncomingCall | null>(null);

   useEffect(() => {

    if (!isAuthenticated) {
        signalRService.offIncomingCall();
        return;
    }

    let mounted = true;

    async function initialize() {

        try {

            await signalRService.start();

            if (!mounted)
                return;

            signalRService.onIncomingCall(
                (call) => {

                    console.log(
                        "📞 Incoming call",
                        call
                    );

                    setIncomingCall(call);

                }
            );

        }
        catch (error) {

            console.error(
                "Failed to initialize calling:",
                error
            );

        }

    }

    void initialize();

    return () => {

        mounted = false;

        signalRService.offIncomingCall();

    };

}, [isAuthenticated]);

    const value = useMemo(
        () => ({
            incomingCall,

            clearIncomingCall() {
                setIncomingCall(null);
            }
        }),
        [incomingCall]
    );

   return (
    <CallingContext.Provider value={value}>
        {children}

        {incomingCall && (
            <IncomingCallModal
                call={incomingCall}
                onAccept={() => {
                    console.log(
                        "Accept call:",
                        incomingCall
                    );
                }}
                onReject={() => {
                    console.log(
                        "Reject call:",
                        incomingCall
                    );

                    setIncomingCall(null);
                }}
            />
        )}
    </CallingContext.Provider>
);
}

export function useCalling() {

    const context =
        useContext(CallingContext);

    if (!context) {

        throw new Error(
            "useCalling must be used inside CallingProvider"
        );

    }

    return context;
}