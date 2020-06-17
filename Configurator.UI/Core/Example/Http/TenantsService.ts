import { EntityHttpService } from "codeshell/http";
import { Injectable } from "@angular/core";
import { ServerConfig } from "Example/ServerConfig";
import { SubmitResult } from "../../codeshell/helpers";
import { ConfigHttpService } from "./ConfigHttpService";

@Injectable()
export class TenantsService extends ConfigHttpService {

    protected get BaseUrl(): string {
        return this.CurrentAppUrl + "/apiAction/Tenants";
    }

    CreateTenant(dto: any): Promise<SubmitResult> {
        return this.PostAs<SubmitResult>("Create", dto);
    }

    Render(connectionId: string): Promise<SubmitResult> {
        debugger;
        return this.PostAs<SubmitResult>("Render", connectionId);
    }
}
