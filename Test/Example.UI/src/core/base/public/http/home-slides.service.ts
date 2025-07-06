import { Injectable } from "@angular/core";
import { EntityHttpService } from "codeshell/http";
import { SubmitResult } from "codeshell/results";

@Injectable()
export class HomeSlidesService extends EntityHttpService {

    protected get BaseUrl(): string {
        return "/apiAction/HomeSlides";
    }

    SetSorting(ids:number[]):Promise<SubmitResult>{
        return this.Post("SetSorting",ids);
    }

}
