import { EntityHttpService } from "codeshell/http";
import { Injectable } from "@angular/core";

@Injectable()
export class PublicContentsService extends EntityHttpService {

    protected get BaseUrl(): string {
        return "/apiAction/PublicContents";
    }

    GetByCode(code:string,lang:string):Promise<any>{
        return this.Get("GetByCode",{code:code,lang:lang});
    }
}
