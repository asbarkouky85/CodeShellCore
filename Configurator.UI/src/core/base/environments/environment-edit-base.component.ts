import { EditComponentBase } from "codeshell/base-components";
import { EnvironmentsService } from "@base/generations/http/environments.service";
import { Component } from "@angular/core";
import { List_RemoveItem } from "codeshell/data";

@Component({ template: "" })
export abstract class EnvironmentEditBase extends EditComponentBase {

	Service = new EnvironmentsService();
	dbs: any[] = [];

	uploadTypes = [
		{ id: "FS", name: "File System" },
		{ id: "FTP", name: "FTP Server" }
	];

	DefaultModel() { return { connectionParams: {}, upload: {} } }

	SubmitAsync() {
		return this.Service.Put("Put", this.model);
	}

	type_change() {
		console.log(this.model.upload)
	}

	dbItem_change(item: any) {
		if (item.newItem && item.name) {
			this.model.databases.push(item.name);
			this.initDbs();
		}
	}

	initDbs() {
		if (!this.model.databases) {
			this.model.databases = [];
		}
		this.dbs = [];
		for (let d of this.model.databases) {
			this.dbs.push({ name: d });
		}
		this.dbs.push({ name: null, newItem: true })
	}

	deleteDb(item: any) {
		List_RemoveItem(this.model.databases, item.name);
		console.log(this.model.databases, item.name)
		this.initDbs();
	}
}