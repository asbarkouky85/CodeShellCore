import { TokenData } from "./models";
import { Stored } from "codeshell/helpers";

export class TokenStorage {
    SaveToken(data: TokenData) {
        Stored.Set("TokenData", data);
    }

    LoadToken(): TokenData|null {
        return Stored.Get("TokenData", TokenData);
    }
    
    Clear() {
        Stored.Clear("TokenData");
        localStorage.removeItem("refresh");
    }

    GetRefreshToken(): string | null {
        return localStorage.getItem("refresh");
    }

    SaveRefreshToken(token: string) {
        localStorage.setItem("refresh", token);
    }
}