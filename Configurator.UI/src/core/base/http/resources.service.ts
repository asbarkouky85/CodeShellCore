import { Injectable } from "@angular/core";
import { ConfigHttpService } from "./config-http.service";

@Injectable()
export class ResourcesService extends ConfigHttpService {

    protected get BaseUrl(): string {
        return "/apiAction/Resources";
    }

   

}
