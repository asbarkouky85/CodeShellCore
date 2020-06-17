import { ListComponentBase } from "codeshell/baseComponents";
import { Injectable } from "@angular/core";
import { ResourcesService } from "Example/Http";
import { Shell } from "codeshell/core";
import { ResourceEditBase } from "./ResourceEditBase";
import { ComponentRequest } from "codeshell/helpers";

@Injectable()
export abstract class ResourceListBase extends ListComponentBase {
    get Service(): ResourcesService { return Shell.Injector.get(ResourcesService); }

    Create() {
        var req: ComponentRequest<ResourceEditBase> = { Identifier: "AddModal" };

        this.GetComponent(req).then(comp => {
            comp.DataSubmitted = res => {
                comp.Show = false;
                this.LoadData();
            }
            comp.BindAsync({}).then(d => {
                comp.Show = true;
            })
        })
    }

    Edit(model: any) {
        var req: ComponentRequest<ResourceEditBase> = { Identifier: "EditModal" };

        this.GetComponent(req).then(comp => {
            comp.DataSubmitted = res => {
                comp.Show = false;
                this.LoadData();
            }
            comp.FillAsync(model.id).then(d => {
                comp.Show = true;
            })
        })
    }
}