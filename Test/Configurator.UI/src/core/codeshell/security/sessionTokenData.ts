import { TokenStorage } from "./tokenStorage";
import { TokenData } from "./models";
import { StorageType, Stored } from "codeshell/services";

export class SessionTokenData extends TokenStorage {

    LoadToken(): TokenData | null {

        return Stored.Get("TokenData", TokenData, StorageType.Session);
    }

    SaveToken(data: TokenData) {

        Stored.Set("TokenData", data, StorageType.Session);
    }

    Clear() {
        Stored.Clear("TokenData", StorageType.Session);
        localStorage.removeItem("refresh");
    }
}