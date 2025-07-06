import { ListComponentBase } from "codeshell/base-components";
import { EnvironmentsService } from "@base/generations/http/environments.service";
import { Component, ViewChild } from "@angular/core";
import { EnvironmentEditBase } from "./environment-edit-base.component";

@Component({ template: "" })
export abstract class EnvironmentListBase extends ListComponentBase {
	showEdit: boolean = false;
	Service = new EnvironmentsService();

	@ViewChild("editComponent") editComponent?: EnvironmentEditBase;

	ngAfterViewInit() {
		if (this.editComponent) {
			this.editComponent.DataSubmitted = (e) => {
				this.LoadData();
			}
		}
	}

	edit(model: any) {
		if (this.editComponent) {
			console.log(model);
			if (!model.upload)
				model.upload = {};
			this.editComponent.IsNew = false;
			this.editComponent.model = model;
			this.editComponent.initDbs();
			this.showEdit = true;
		}
	}

	add() {
		if (this.editComponent) {
			this.editComponent.Clear();
			this.editComponent.IsNew = true;
			this.editComponent.initDbs();
			this.showEdit = true;
		}
	}

	deleteEnv(env: any) {

	}
}