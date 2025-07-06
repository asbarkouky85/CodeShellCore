import { EntityHttpService } from "codeshell/http";
import { Injectable } from "@angular/core";
import { SubmitResult } from "codeshell/helpers";

@Injectable()
export class HomeSlidesService extends EntityHttpService {

    protected get BaseUrl(): string {
        return "/apiAction/HomeSlides";
    }

    SetSorting(ids:number[]):Promise<SubmitResult>{
        return this.Post("SetSorting",ids);
    }

}
