import { DbCreationRequest } from "@base/dtos";
import { SubmitResult } from "codeshell/results";
import { ConfigHttpService } from "./config-http.service";

export class BuilderService extends ConfigHttpService {
    protected get BaseUrl(): string { return "/apiAction/Builder" };

    Init(replace?: boolean|null): Promise<SubmitResult> {
        return this.Post("Init", {}, { replace: replace });
    }

    AddSQLFiles(replace?: boolean|null): Promise<SubmitResult> {
        return this.Post("AddSQLFiles", {}, { replace: replace });
    }

    AddStaticFiles(replace?: boolean|null): Promise<SubmitResult> {
        return this.Post("AddStaticFiles", {}, { replace: replace });
    }

    AddBaseScripts(replace?: boolean|null): Promise<SubmitResult> {
        return this.Post("AddBaseScripts", {}, { replace: replace });
    }

    AddShellComponents(replace?: boolean|null): Promise<SubmitResult> {
        return this.Post("AddShellComponents", {}, { replace: replace })
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

    FixPages(): Promise<SubmitResult> {
        return this.Post("FixPages", {});
    }

}