import { IDictionary, ListItem, TmpFileData } from "codeshell/helpers";
import { UserDTO } from "codeshell/security";


export class UserRole {
    roleId: number=0;
    userId: number=0;
}

export class User{
    id: number = 0;
    tenantId?: number;
    userRoles: ListItem[] = [];
    parties: ListItem[] = [];
    email?: string;
    mobile?:string;
    name?: string;
    userType: number = 0;
    logonName?: string;
    password?: string;
    passwordConfim?: string;
    role?: any;
    customerId?: number;
    appId?: number;
    personId?: number;
    photoFile?: TmpFileData;
    gender?: number;
}

export class FMSUserDTO extends UserDTO{
    parties?: number[];
    customerLogo?: string;
    profilePicture?: string;

}

export class RoleResourceAction extends ListItem {
    resourceActionId: number=0;
    roleId: number=0;
    state: string = "Detached";
}

export class Perm extends ListItem{

    public resourceId: number=0;
    public roleId: number=0;
    public canViewDetails: boolean = false;
    public canUpdate: boolean = false;
    public canInsert: boolean = false;
    public canDelete: boolean = false;
    collectionId?: number;

    SetAll(set: boolean) {
        this.canDelete = set;
        this.canInsert = set;
        this.canUpdate = set;
        this.canViewDetails = set;
    }
}

