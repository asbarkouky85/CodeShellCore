import { EntityHttpService } from "codeshell/http";
import { ServerConfig } from "Example/ServerConfig";
import { DbCreationRequest } from "Example/Dtos"
import { SubmitResult } from "codeshell/helpers";
import { ConfigHttpService } from "./ConfigHttpService";

export class SqlCommandsService extends ConfigHttpService {
    get BaseUrl() {
        return this.CurrentAppUrl + "/apiAction/SqlCommands";
    }

    CreateTenantDatabase(dto: DbCreationRequest): Promise<SubmitResult> {
        return this.Post("CreateTenantDatabase", dto);
    }

    UpdateTenantDatabse(dto: DbCreationRequest): Promise<SubmitResult> {
        return this.Post("UpdateTenantDatabse", dto);
    }

    GetEnvironments(): Promise<any[]> {
        return this.Get("GetEnvironments");
    }
}