import axios from "axios";
import { tokenStorage } from "../auth/tokenStorage";

export const identityApi = axios.create({
    baseURL: "http://localhost:5176/api",
});

identityApi.interceptors.request.use(config => {
    const token = tokenStorage.getAccessToken();

    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }

    return config;
});