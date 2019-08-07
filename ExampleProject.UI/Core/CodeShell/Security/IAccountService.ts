import { DataHttpService } from "CodeShell/Http";
import { LoginResult } from "./Models";


export abstract class IAccountService extends DataHttpService {
    Login(model: any): Promise<LoginResult> {
        return this.PostAs<LoginResult>("Login", model);
    }

}