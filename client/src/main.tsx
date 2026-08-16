import React from "react";
import ReactDOM from "react-dom/client";
import { RouterProvider } from "react-router-dom";
import "./styles/theme.css";

import { router } from "./app/router";
import { AuthProvider } from "./auth/AuthContext";
import { CallingProvider } from "./features/calling/CallingContext";

import "./index.css";

ReactDOM.createRoot(document.getElementById("root")!).render(
    <React.StrictMode>
        <AuthProvider>
            <CallingProvider>
                <RouterProvider router={router} />
            </CallingProvider>
        </AuthProvider>
    </React.StrictMode>
);