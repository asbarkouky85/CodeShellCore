import { EntityHttpService } from "codeshell/http";
import { Injectable } from "@angular/core";
import { ConfigHttpService } from "./ConfigHttpService";
import { ComponentRequest } from "codeshell/helpers";
import { ResourceEditBase } from "Example/Resources/ResourceEditBase";

@Injectable()
export class ResourcesService extends ConfigHttpService {

    protected get BaseUrl(): string {
        return this.CurrentAppUrl + "/apiAction/Resources";
    }

   

}
