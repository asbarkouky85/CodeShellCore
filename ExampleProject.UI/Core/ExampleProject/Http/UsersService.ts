import { EntityHttpService } from "CodeShell/Http";
import { Injectable } from "@angular/core";

@Injectable()
export class UsersService extends EntityHttpService {

    protected get BaseUrl(): string {
        return "/apiAction/Users";
    }

}
