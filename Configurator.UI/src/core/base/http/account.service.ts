import { AppInfo } from "@base/models";
import { AccountServiceBase } from "codeshell/http";

export class AccountService extends AccountServiceBase {
    protected BaseUrl: string = "apiAction/Account";

    GetApps(): Promise<AppInfo[]> {
        return this.Get("GetApps");
    }

}