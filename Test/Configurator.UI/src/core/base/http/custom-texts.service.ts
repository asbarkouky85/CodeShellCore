import { Injectable } from "@angular/core";
import { CustomTextRequest } from "@base/dtos";
import { LoadOptions, ListItem } from "codeshell/data";
import { LoadResult } from "codeshell/results";
import { ConfigHttpService } from "./config-http.service";

@Injectable()
export class CustomTextsService extends ConfigHttpService {

     get BaseUrl(): string {
        return "/apiAction/CustomTexts";
    }

    async GetData(req: CustomTextRequest, opts: LoadOptions): Promise<LoadResult> {
        var data = await this.PostAs<LoadResult>("Get", req, opts);
        for (var i in data.list) {
            data.list[i] = Object.assign(new ListItem, data.list[i]);
        }
        return data;
    }

    SaveChanges(items: any[]) {
        return this.Post("SaveChanges", items);
    }
}
