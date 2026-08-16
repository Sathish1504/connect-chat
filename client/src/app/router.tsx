import { createBrowserRouter } from "react-router-dom";

import LoginPage from "../pages/LoginPage";
import DashboardPage from "../pages/DashboardPage";

import ProtectedRoute from "../auth/ProtectedRoute";

import ProfilePage from "../pages/ProfilePage";

export const router = createBrowserRouter([
    {
        path: "/",
        element: <LoginPage />
    },
    {
        path: "/dashboard",
        element: (
            <ProtectedRoute>
                <DashboardPage />
            </ProtectedRoute>
        )
    },
    {
        path: "/profile",
        element: (
            <ProtectedRoute>
                <ProfilePage />
            </ProtectedRoute>
        )
    }
]);