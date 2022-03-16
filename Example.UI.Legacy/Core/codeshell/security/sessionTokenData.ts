import { TokenStorage } from "./tokenStorage";
import { TokenData } from "./models";
import * as Cookies from "js-cookie";
import { Stored } from "codeshell/helpers/stored";

export class SessionTokenData extends TokenStorage {

    LoadToken(): TokenData | null {
        
        return Stored.Get_SS("TokenData", TokenData);
    }

    SaveToken(data: TokenData) {
       
        Stored.Set_SS("TokenData", data);
    }

    Clear() {
        Stored.Clear_SS("TokenData");
        localStorage.removeItem("refresh");
    }
}