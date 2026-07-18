import axios from "axios";
import { tokenStorage } from "../auth/tokenStorage";

export const chatApi = axios.create({
    baseURL: "https://localhost:7290/api",
});

chatApi.interceptors.request.use(config => {
    const token = tokenStorage.getAccessToken();

    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }

    return config;
});