import { Component, Injectable, Injector } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { AppComponentBase } from "@base/app-component-base.component";
import { CustomTextRequest } from "@base/dtos";
import { TextTypes } from "@base/enumerations";
import { CustomTextsService } from "@base/http";
import { Tenant } from "@base/models";
import { Shell } from "codeshell";
import { ListComponentBase } from "codeshell/base-components";
import { LoadOptions, ListItem } from "codeshell/data";
import { LoadResult } from "codeshell/results";

@Component({template:''})
export abstract class CustomTextListBase extends ListComponentBase {
	Service = new CustomTextsService();

	request: CustomTextRequest = new CustomTextRequest();
	currentType?: string;
	get App(): AppComponentBase { return Shell.Main as AppComponentBase; }
	options: LoadOptions = { Showing: 20, Skip: 0 };

	constructor(rt: ActivatedRoute, res: Injector) {
		super(rt, res);
		this.App.OnTenantChanged.asObservable().subscribe(e => {
			this.tenantChanged(e);
		});
	}

	tenantChanged(ten: Tenant | undefined) { }

	ngOnInit() {
		this.App.AppDataReady().then(r => {
			super.ngOnInit();
		})
	}

	getTenantId(): number | undefined {
		return this.App.GetTenantId();
	}

	LoadDataPromise(): Promise<LoadResult> {
		var ten = this.getTenantId();
		if (ten && this.request.locale && this.request.type != 0) {
			this.request.tenantId = ten;
			return this.Service.GetData(this.request, this.options);
		} else {
			return Promise.resolve(new LoadResult());
		}
	}

	isActive(locale: string, type: string) {

		return this.request.locale == locale && this.currentType == type;
	}

	text_change(model: ListItem) {
		if ((model as any).value == '') {
			model.SetRemoved();
		} else {
			model.AddToChangeList();
		}

	}

	private _getType(type: string): number {
		switch (type) {
			case "Words":
				return TextTypes.Words;
			case "Columns":
				return TextTypes.Columns;
			case "Pages":
				return TextTypes.Pages;
			case "Messages":
				return TextTypes.Messages;

		}
		return 0;
	}

	Load(locale: string, type: string) {

		this.request.locale = locale;
		this.request.type = this._getType(type);
		this.currentType = type;
		this.LoadData();
	}

	SaveChanges() {
		var items = ListItem.GetChangedItems(this.list);
		this.Service.SaveChanges(items).then(e => {
			this.LoadData();
		})
	}
}