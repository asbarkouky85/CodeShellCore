import { DataHttpService } from "CodeShell/Http";
import { Injectable } from "@angular/core";

@Injectable()
export class UsersService extends DataHttpService {

    protected get BaseUrl(): string {
        return "/apiAction/Users";
    }

}
