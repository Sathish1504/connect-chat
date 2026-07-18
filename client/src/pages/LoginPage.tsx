import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { login } from "../features/authentication/authService";
import { tokenStorage } from "../auth/tokenStorage";

export default function LoginPage() {

    const navigate = useNavigate();

    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");

    const [loading, setLoading] = useState(false);

    const [error, setError] = useState("");

    async function handleSubmit(e: React.FormEvent) {

        e.preventDefault();

        setLoading(true);
        setError("");

        try {

            const result = await login({
                email,
                password
            });

            tokenStorage.setTokens(
                result.accessToken,
                result.refreshToken);

            navigate("/dashboard");

        }
        catch {

            setError("Invalid email or password.");

        }
        finally {

            setLoading(false);

        }
    }

    return (

        <div style={{ padding: 40 }}>

            <h1>ConnectChat Login</h1>

            <form onSubmit={handleSubmit}>

                <input
                    type="email"
                    placeholder="Email"
                    value={email}
                    onChange={e => setEmail(e.target.value)}
                />

                <br /><br />

                <input
                    type="password"
                    placeholder="Password"
                    value={password}
                    onChange={e => setPassword(e.target.value)}
                />

                <br /><br />

                <button disabled={loading}>
                    {loading ? "Signing In..." : "Login"}
                </button>

            </form>

            <br />

            <span style={{ color: "red" }}>
                {error}
            </span>

        </div>

    );
}