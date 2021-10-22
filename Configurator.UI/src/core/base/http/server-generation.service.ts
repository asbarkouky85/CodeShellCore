import { RenderDTO, DbCreationRequest, PreviewData } from "@base/dtos";
import { SubmitResult } from "codeshell/results";
import { ConfigHttpService } from "./config-http.service";

export class ServerGenerationService extends ConfigHttpService {

    get BaseUrl() {
        return "/apiAction/Generation";
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

    Mapping():Promise<SubmitResult>{
        return this.GetAs<SubmitResult>("Mapping");
    }
    
}