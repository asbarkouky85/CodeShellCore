import { EditComponentBase } from "./editComponentBase";
import { DTO, SubmitResult } from "../helpers";
import { Component } from '@angular/core';


@Component({ template: '' })
export abstract class DTOEditComponentBase extends EditComponentBase {
    model: any = {};

    get ModelId(): number { return this.model.entity.id; }
    set ModelId(val: number) { this.model.entity.id=val; }

    DefaultModel() {
        return new DTO<any>();
    }
    SubmitNewAsync(): Promise<SubmitResult> {

        return this.Service.Save("post", this.model.entity)
    }
    SubmitUpdateAsync(): Promise<SubmitResult> {
        return this.Service.Update("put", this.model.entity)
    }
}