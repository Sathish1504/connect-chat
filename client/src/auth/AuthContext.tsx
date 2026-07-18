import {
    createContext,
    useContext,
    useEffect,
    useMemo,
    useState,
    type ReactNode
} from "react";

import { tokenStorage } from "./tokenStorage";

import { jwtDecode } from "jwt-decode";
import type { AuthUser } from "./AuthUser";

interface AuthContextType {
    isAuthenticated: boolean;
    user?: AuthUser;
    login: (accessToken: string, refreshToken: string) => void;
    logout: () => void;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);
interface JwtPayload {
    sub: string;
    unique_name: string;
    email: string;
}

export function AuthProvider({ children }: { children: ReactNode }) {

    const [isAuthenticated, setIsAuthenticated] = useState(false);
    const [user, setUser] = useState<AuthUser>();

   useEffect(() => {

    const token = tokenStorage.getAccessToken();

    if (!token)
        return;

    try {

        const payload = jwtDecode<JwtPayload>(token);

        setUser({
            id: payload.sub,
            username: payload.unique_name,
            email: payload.email
        });

        setIsAuthenticated(true);

    }
    catch {

        tokenStorage.clear();

        setIsAuthenticated(false);

    }

}, []);

    const value = useMemo(() => ({

    isAuthenticated,
    user,

    login(accessToken: string, refreshToken: string) {

        tokenStorage.setTokens(accessToken, refreshToken);

        const payload = jwtDecode<JwtPayload>(accessToken);

        setUser({
            id: payload.sub,
            username: payload.unique_name,
            email: payload.email
        });

        setIsAuthenticated(true);
    },

    logout() {

        tokenStorage.clear();

        setUser(undefined);

        setIsAuthenticated(false);
    }

}), [isAuthenticated, user]);

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