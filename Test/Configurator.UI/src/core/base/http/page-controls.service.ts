import { Injectable } from "@angular/core";
import { LoadOptions } from "codeshell/data";
import { LoadResult, SubmitResult } from "codeshell/results";
import { ConfigHttpService } from "./config-http.service";

@Injectable()
export class PageControlsService extends ConfigHttpService {

    protected get BaseUrl(): string {
        return "/apiAction/PageControls";
    }


    GetControlByPageId(opts: LoadOptions, PageId: any): Promise<LoadResult>{
        opts.Showing = 0;
        opts.Skip = 0;
        if (PageId != 0 && PageId != undefined)
            LoadOptions.AddFilter(opts, { FilterType: "reference", Ids: [PageId], MemberName: "PageId" })
        return this.Get("GetControlByPageId", opts);
    }

    UpdatePageControls(dto: any): Promise<SubmitResult> {
        return this.PostAs<SubmitResult>("UpdatePageControls", dto);
    }

}
