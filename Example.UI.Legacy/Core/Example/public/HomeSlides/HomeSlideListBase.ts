import { ListComponentBase, EditComponentBase } from "codeshell/baseComponents";
import { Injectable, ViewChild } from "@angular/core";
import { HomeSlidesService } from "../Http";
import { LoadOptions } from "codeshell/helpers";

@Injectable()
export abstract class HomeSlideListBase extends ListComponentBase {
    Service = new HomeSlidesService();

    @ViewChild("AddComponent")
    AddComponent?: EditComponentBase;

    showEditor: boolean = false;

    OnAdding: () => void = () => { };
    OnEditing: (model: any) => void = m => { };

	options: LoadOptions = { Showing: 20, Skip: 0, OrderProperty: "DisplayOrder" };

	ngOnInit() {
		
		super.ngOnInit();
	}

	AfterLoad() {
		$(".sortable").sortable({
			beforeStop: (ev: Event) => {

				var el = ev.target as HTMLElement;
				var q = el.querySelectorAll("tr");
				var ids: number[] = [];
				for (var i = 0; i < q.length; i++) {
					var tr = q.item(i);
					if (tr && tr.id)
						ids.push(parseInt(tr.id));
				}
				this.Service.Silent = true;
				this.Service.SetSorting(ids).then(d => this.Service.Silent = false);
				console.log(ids);
			}
		});
    }

    StartAdd() {
        if (this.AddComponent) {
            this.AddComponent.Clear();
        }
        this.OnAdding();
        this.showEditor = true;

    }

    Edit(model: any) {
        if (this.AddComponent) {
            this.AddComponent.FillAsync(model.id).then(d => {
                this.showEditor = true;
            })
        }
        this.OnEditing(model);
    }

    isActive_Change(model: any) {
        this.Service.Silent = true;
        this.Service.SetActive(model.id, model.isActive).then(d => this.Service.Silent = false);
    }
}