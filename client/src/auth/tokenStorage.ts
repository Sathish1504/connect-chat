const ACCESS_TOKEN = "access_token";
const REFRESH_TOKEN = "refresh_token";

export const tokenStorage = {

    getAccessToken() {
        return localStorage.getItem(ACCESS_TOKEN);
    },

    setTokens(accessToken: string, refreshToken: string) {
        localStorage.setItem(ACCESS_TOKEN, accessToken);
        localStorage.setItem(REFRESH_TOKEN, refreshToken);
    },

    clear() {
        localStorage.removeItem(ACCESS_TOKEN);
        localStorage.removeItem(REFRESH_TOKEN);
    }
};