import { Injectable } from "@angular/core";
import { ParameterRequestDTO } from "@base/dtos";
import { LoadOptions } from "codeshell/data";
import { LoadResult } from "codeshell/results";
import { ConfigHttpService } from "./config-http.service";

@Injectable()
export class PageParametersService extends ConfigHttpService {

    protected get BaseUrl(): string {
        return "/apiAction/PageParameters";
    }


    GetReferences(req:ParameterRequestDTO,op:LoadOptions):Promise<LoadResult>{
        return this.Post("GetReferences",req,op);
    }
}
