import { EntityHttpService } from "codeshell/http";
import { SubmitResult } from "codeshell/helpers";

export class UsersService extends EntityHttpService {
    protected get BaseUrl(): string {
        return "/apiAction/Users";
    }
    public GetTenantApps(): Promise<any> {
        return this.Get("GetTenantApps");
    }

    public GetUserRole(userId: number): Promise<any> {
        return this.Get("GetUserRole", userId);
    }

    public CheckIsUserExist(model: any): Promise<SubmitResult> {
        return this.Post("CheckUserexist", model);
    }

    public ChangePassword(model: any): Promise<SubmitResult> {
        return this.Save("ChangePassword", model);
    }
}