import { Component, Injector } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { Shell } from "codeshell";
import { BaseComponent } from "codeshell/base-components";
import { AppComponentBase } from "./app-component-base.component";
import { Tenant } from "./models";

@Component({ template: '' })
export abstract class TenantComponentBase extends BaseComponent {
    //model: any = {};


    get App(): AppComponentBase { return Shell.Main as AppComponentBase; }

    constructor(rt: ActivatedRoute, res: Injector) {
        super(rt, res);
        this.App.OnTenantChanged.asObservable().subscribe(e => {
            this.tenantChanged(e);
        });


    }

    Refresh() {
        this.OnReady();
    }

    tenantChanged(ten: Tenant | undefined) { }

    ngOnInit() {
        this.App.AppDataReady().then(r => {
            this.OnReady()
        })
    }

    OnReady() {

    }

    getTenantId(): number | undefined {
        return this.App.GetTenantId();
    }
}