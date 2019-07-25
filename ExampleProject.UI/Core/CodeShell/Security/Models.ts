export enum ResourceActions { view, details, update, insert, delete }

export class DomainDataProvider {
    Domains: DomainData[] = [];
    constructor(domains: DomainData[]) {
        this.Domains = domains;
    }
}

export class DomainData {
    name: string = "";
    children: any[] = [];
}

export class RouteData {
    name: string = "";
    navigate: boolean = false;
    resource: string = "";
    action: ResourceActions | string = ResourceActions.view;
    apps?: string[];
    
    get IsAnonymous(): boolean { return this.action == "anonymous"; }
    get AllowAll(): boolean { return this.action == "allowAll"; }
}

export class UserDTO {
    public userId: number=0;
    public tenantId: number=0;
    public tenantCode: string = "";
    public name: string = "";
    public logonName: string = "";
    public userTypeString: string = "";
    public apps: string[] = [];
    public permissions: { [key: string]: Permission } = {};

    
}

export class LoginResult {
    public success: boolean = false;
    public message: string = "";
    public userData: UserDTO = new UserDTO;
    public token: string = "";
    public tokenExpiry: Date = new Date;
}

export class TokenData {
    public Token: string = "";
    public Expiry: Date = new Date;

    public IsExpired(): boolean {
        return new Date() > new Date(this.Expiry)
    }
}

export class Permission {
    public actions: string[];
    public insert: boolean = false;
    public update: boolean = false;
    public delete: boolean = false;
    public details: boolean = false;
    public view: boolean = true;

    constructor() {
        this.actions = [];
    }

    public static get Anonymous(): Permission {
        return Object.assign(new Permission,{
            insert: true,
             update: false,
             delete : false,
             details : false,
             view: true
        });
    }

    public static get Denied(): Permission {
        return Object.assign(new Permission, {
            insert: false,
            update: false,
            delete: false,
            details: false,
            view: false
        });
    }

    public static get FullAccess(): Permission {
        return Object.assign(new Permission, {
            insert: true,
            update: true,
            delete: true,
            details: true,
            view: true
        });
    }

    get canSubmit(): boolean { return this.insert || this.update || this.delete; }
    
    public Can(ac: number | string): boolean {
        if (ac == "anonymous" || ac=="allowAll")
            return true;
        
        switch (ac) {
            case 4:
                return this.delete;

            case 2:
                return this.update;

            case 3:
                return this.insert;

            case 1:
                return this.details;

            case 0:
                return this.view;

            default:
                if (this.actions == null)
                    return false;
                return this.actions.indexOf(ac as string) > -1;
        }
    }
}