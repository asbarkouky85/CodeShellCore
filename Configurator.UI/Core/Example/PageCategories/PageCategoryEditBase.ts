import { EditComponentBase } from "codeshell/baseComponents";
import { Injectable } from "@angular/core";
import { PageCategoriesService } from "Example/Http";
import { Shell } from "codeshell/core";
import { BaseComponentList } from "../Models";
import { ServerGenerationService } from "Example/Http/ServerGenerationService";

@Injectable()
export abstract class PageCategoryEditBase extends EditComponentBase{
    get Service(): PageCategoriesService { return Shell.Injector.get(PageCategoriesService); }
    
    Gen = new ServerGenerationService();
    ActiveTab = "Controls";
    baseComponentList: BaseComponentList[] = [
        { name: "List" },
        { name: "Edit" },
        { name: "Tree" },
        { name: "Select" },
    ];

    async LoadLookupsAsync(opts: any): Promise<any> {
        var res = await super.LoadLookupsAsync(opts);
        res.BaseComponent = this.baseComponentList;
        return res;
    }

    Process() {
        this.Gen.CollectTemplateData(this.model.id).then(d => {
            this.Fill(this.model.id);
        });
    }
}