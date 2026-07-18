import { api } from "../../api/axios";
import { ApiEndpoints } from "../../api/endpoints";
import type { LoginRequest } from "./types/LoginRequest";
import type { LoginResponse } from "./types/LoginResponse";

export async function login(request: LoginRequest): Promise<LoginResponse> {
    const response = await api.post<LoginResponse>(
        ApiEndpoints.login,
        request);

    return response.data;
}