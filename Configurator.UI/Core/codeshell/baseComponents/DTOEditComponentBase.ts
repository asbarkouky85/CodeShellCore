import { EditComponentBase } from "./editComponentBase";
import { DTO, SubmitResult } from "../helpers";

export abstract class DTOEditComponentBase extends EditComponentBase{
    model: any = {};
    protected DefaultModel() {
        return new DTO<any>();
    }
    SubmitNewAsync(): Promise<SubmitResult> {

        return this.Service.Save("post", this.model.entity)
    }
    SubmitUpdateAsync(): Promise<SubmitResult> {
        return this.Service.Update("put", this.model.entity)
    }
}