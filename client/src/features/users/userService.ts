import { identityApi } from "../../api/identityApi";
import { ApiEndpoints } from "../../api/endpoints";
import type { User } from "../users/types/user";

export async function getUsers(): Promise<User[]> {

   const response = await identityApi.get<User[]>(
        ApiEndpoints.users
    );

    return response.data;
}

export async function getUserById(
    id: string
): Promise<User> {

    const response = await identityApi.get<User>(
        `/users/${id}`
    );

    return response.data;

}