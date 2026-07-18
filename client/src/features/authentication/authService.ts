import { identityApi } from "../../api/identityApi";
import { ApiEndpoints } from "../../api/endpoints";
import type { LoginRequest } from "./types/LoginRequest";
import type { LoginResponse } from "./types/LoginResponse";

export async function login(
    request: LoginRequest
): Promise<LoginResponse> {

    const response = await identityApi.post<LoginResponse>(
        ApiEndpoints.login,
        request
    );

    return response.data;
}