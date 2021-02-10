import { SubmitResult } from "codeshell/helpers";
import { ConfigHttpService } from "./ConfigHttpService";
import { DbCreationRequest } from "Example/Dtos";

export class BuilderService extends ConfigHttpService {
    protected get BaseUrl(): string { return this.CurrentAppUrl + "/apiAction/Builder" };

    Init(): Promise<SubmitResult> {
        return this.Post("Init", {});
    }

    AddSQLFiles(): Promise<SubmitResult> {
        return this.Post("AddSQLFiles", {});
    }

    AddStaticFiles(): Promise<SubmitResult> {
        return this.Post("AddStaticFiles", {});
    }

    AddBaseScripts(): Promise<SubmitResult> {
        return this.Post("AddBaseScripts", {});
    }

    WriteWebPackFiles(): Promise<SubmitResult> {
        return this.Post("WriteWebPackFiles", {});
    }

    PrepEnvironment(packProd: boolean): Promise<SubmitResult> {
        return this.Post("PrepEnvironment", {}, { packProd: packProd });
    }

    ClearOlderBundles(req: DbCreationRequest): Promise<SubmitResult> {
        return this.Post("ClearOlderBundles", req);
    }

    InitializeResx(): Promise<SubmitResult> {
        return this.Post("InitializeResx", {});
    }

    SyncLanguages(): Promise<SubmitResult> {
        return this.Post("SyncLanguages", {});
    }

}