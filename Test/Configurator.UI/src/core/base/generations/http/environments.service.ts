import { Injectable } from "@angular/core";
import { ConfigHttpService } from "@base/http/config-http.service";
import { EntityHttpService } from "codeshell/http";

@Injectable()
export class EnvironmentsService extends ConfigHttpService {

    protected get BaseUrl(): string {
        return "/apiAction/Environments";
    }

}
