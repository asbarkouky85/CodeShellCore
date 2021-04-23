import { Permission } from './permission';

export class UserDTO {
    id: number = 0;
    userId: string = "";
    tenantId?: number;
    tenantCode: string = "";
    name: string = "";
    logonName: string = "";
    userTypeString: string = "";
    apps: string[] = [];
    app?: string;
    photo?: string;
    permissions: { [key: string]: Permission } = {};
    entityLinks: any = {};
    personId?: number;
}

export class LoginResult {
    success: boolean = false;
    message: string = "";
    userData: UserDTO = new UserDTO;
    token: string = "";
    tokenExpiry: Date = new Date;
    refreshToken?: string | null;
}

export class TokenData {
    Token: string = "";
    Expiry: Date = new Date;
    RefreshToken?: string | null;

    IsExpired(): boolean {
        return new Date() > new Date(this.Expiry)
    }
}


