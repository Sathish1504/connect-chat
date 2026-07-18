import {
    createContext,
    useContext,
    useEffect,
    useMemo,
    useState,
    type ReactNode
} from "react";

import { tokenStorage } from "./tokenStorage";

interface AuthContextType {
    isAuthenticated: boolean;
    login: (accessToken: string, refreshToken: string) => void;
    logout: () => void;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export function AuthProvider({ children }: { children: ReactNode }) {

    const [isAuthenticated, setIsAuthenticated] = useState(false);

    useEffect(() => {
        setIsAuthenticated(!!tokenStorage.getAccessToken());
    }, []);

    const value = useMemo(() => ({

        isAuthenticated,

        login(accessToken: string, refreshToken: string) {

            tokenStorage.setTokens(accessToken, refreshToken);

            setIsAuthenticated(true);
        },

        logout() {

            tokenStorage.clear();

            setIsAuthenticated(false);
        }

    }), [isAuthenticated]);

    return (

        <AuthContext.Provider value={value}>
            {children}
        </AuthContext.Provider>

    );
}

export function useAuth() {

    const context = useContext(AuthContext);

    if (!context) {
        throw new Error("useAuth must be used inside AuthProvider");
    }

    return context;
}