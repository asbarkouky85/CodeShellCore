import { Component } from "@angular/core";
import { ResourcesService } from "@base/http";
import { Shell } from "codeshell";
import { ListComponentBase } from "codeshell/base-components";
import { ComponentRequest } from "codeshell/components";
import { ResourceEditBase } from "./resource-edit-base.component";

@Component({ template: '' })
export abstract class ResourceListBase extends ListComponentBase {
    Service = new ResourcesService();

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