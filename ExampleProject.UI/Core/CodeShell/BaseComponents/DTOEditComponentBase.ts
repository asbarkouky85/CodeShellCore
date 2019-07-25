import { EditComponentBase } from "./EditComponentBase";
import { DTO, SubmitResult } from "CodeShell/Helpers";
import { Services } from "@angular/core/src/view";

export abstract class DTOEditComponentBase extends EditComponentBase{
    model: any = {};
    protected DefaultModel() {
        return new DTO<any>();
    }
    SubmitNewAsync(): Promise<SubmitResult> {
        debugger
        return this.Service.Save("post", this.model.entity)
    }
    SubmitUpdateAsync(): Promise<SubmitResult> {
        return this.Service.Update("put", this.model.entity)
    }
}