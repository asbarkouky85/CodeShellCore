import { DbCreationRequest } from "@base/dtos";
import { SubmitResult } from "codeshell/results";
import { ConfigHttpService } from "./config-http.service";

export class SqlCommandsService extends ConfigHttpService {
    get BaseUrl() {
        return "/apiAction/SqlCommands";
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