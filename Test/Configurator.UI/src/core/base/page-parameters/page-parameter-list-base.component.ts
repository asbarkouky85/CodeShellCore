import { Component, Injector } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { AppComponentBase } from "@base/app-component-base.component";
import { ParameterRequestDTO } from "@base/dtos";
import { ParameterRequestTypes } from "@base/enumerations";
import { PageParametersService } from "@base/http";
import { Shell } from "codeshell";
import { ListComponentBase } from "codeshell/base-components";
import { LoadOptions } from "codeshell/data";
import { LoadResult } from "codeshell/results";

@Component({template:''})
export abstract class PageParameterListBase extends ListComponentBase {
	Service = new PageParametersService();
	model: any = {};
	get App(): AppComponentBase { return Shell.Main as AppComponentBase; }

	Type?: ParameterRequestTypes;
	filterModel: ParameterRequestDTO = new ParameterRequestDTO;

	options: LoadOptions = { Showing: 15, Skip: 0, OrderProperty: "PageViewPath" };

	constructor(rt: ActivatedRoute, res: Injector) {
		super(rt, res);
		this.App.OnTenantChanged.asObservable().subscribe(e => {
			this.tenantChanged(e);
		});
	}

	ClearFilter() {
		this.filterModel = new ParameterRequestDTO();
		this.options.SearchTerm = "";
		this.LoadData();
	}

	tenantChanged(e: any) {
		this.LoadData();
	}

	async LoadDataPromise(): Promise<LoadResult> {
		await this.App.AppDataReady();
		if (!this.App.UseState.tenantCode)
			Promise.resolve(new LoadResult());
		this.filterModel.tenantCode = this.App.UseState.tenantCode;
		if (this.Type)
			this.filterModel.type = this.Type;
		return this.Service.GetReferences(this.filterModel, this.options);
	}
}