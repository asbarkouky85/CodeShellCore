import { ConfigHttpService } from "./ConfigHttpService";
import { ServerConfig } from "Example/ServerConfig";
import { SubmitResult } from "codeshell/helpers";
import { RenderDTO, DbCreationRequest, PreviewData } from "Example/Dtos";

export class ServerGenerationService extends ConfigHttpService {

    get BaseUrl() {
        return this.CurrentAppUrl + "/apiAction/Generation";
    }

    ProcessForPage(pageId: number): Promise<SubmitResult> {

        return this.Get("ProcessForPage", { pageId: pageId })
    }

    RenderPage(pageId: number): Promise<SubmitResult> {
        return this.Get("RenderPage/" + pageId);
    }

    Render(dto: RenderDTO): Promise<SubmitResult> {
        return this.PostAs<SubmitResult>("Render", dto);
    }

    Process(dto: RenderDTO): Promise<SubmitResult> {
        return this.PostAs<SubmitResult>("Process", dto);
    }

    RenderTenant(dto: RenderDTO) {
        return this.PostAs<SubmitResult>("RenderTenant", dto);
    }

    ModuleDefinition(dto: RenderDTO) {
        return this.PostAs<SubmitResult>("ModuleDefinition", dto);
    }

    SyncTenants(sourceTenant: number, targetTenant: number): Promise<SubmitResult> {
        return this.PostAs<SubmitResult>("SyncTenants", { id1: sourceTenant, id2: targetTenant })
    }

    PublishTenant(req: DbCreationRequest): Promise<SubmitResult> {
        return this.PostAs<SubmitResult>("PublishTenant", req);
    }

    CollectTemplateData(id: number): Promise<SubmitResult> {
        return this.PostAs<SubmitResult>("CollectTemplateData/" + id, {});
    }

    StartTenantPreview(req: DbCreationRequest): Promise<SubmitResult> {
        return this.PostAs<SubmitResult>("StartTenantPreview", req);
    }

    StopPreview(): Promise<SubmitResult> {
        return this.Get("StopPreview");
    }

    GetActivePreview(): Promise<PreviewData> {
        return this.GetAs<PreviewData>("GetActivePreview");
    }

    
}