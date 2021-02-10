import { ConfigHttpService } from "./ConfigHttpService";
import { SubmitResult } from "codeshell/helpers";
import { ServerConfig } from "Example/ServerConfig";
import { DbCreationRequest } from "Example/Dtos";

export class ConfigPagesService extends ConfigHttpService {

    get BaseUrl() {
        return this.CurrentAppUrl + "/apiAction/Pages";
    }

    PageMoved(req: any): Promise<SubmitResult> {
        return this.Post("PageMoved", req);
    }

    TenantCreated(req: DbCreationRequest): Promise<SubmitResult> {
        return this.Post("TenantCreated", req);
    }
}