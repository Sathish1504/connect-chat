import axios from "axios";
import { tokenStorage } from "../auth/tokenStorage";

export const IDENTITY_API_BASE_URL = "http://localhost:5176";

export const identityApi = axios.create({
    baseURL: `${IDENTITY_API_BASE_URL}/api`,
});

identityApi.interceptors.request.use(config => {
    const token = tokenStorage.getAccessToken();

    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }

    return config;
});