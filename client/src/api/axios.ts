import axios from "axios";
import { tokenStorage } from "../auth/tokenStorage";

export const api = axios.create({
    baseURL: "http://localhost:5176/api",
});

api.interceptors.request.use(config => {

    const token = tokenStorage.getAccessToken();

    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }

    return config;
});