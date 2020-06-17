import { BaseComponent } from "codeshell/baseComponents";
import { AppComponentBase } from "./AppComponentBase";
import { Shell } from "codeshell/core";
import { TenantsService } from "./Http";

export abstract class TenantComponentBase extends BaseComponent{
    model: any = {};
    tenants: any[] = [];
    get App(): AppComponentBase { return Shell.Main as AppComponentBase; }
    get TenantService(): TenantsService { return Shell.Injector.get(TenantsService); }

    tenantChanged() {
        this.App.UseState.tenantCode = this.model.tenantCode;
        this.App.SaveState();
    }

    ngOnInit() {
        this.TenantService.Get("Get").then((res: any) => {
            this.tenants = res.list;
            if (this.App.UseState.tenantCode)
                this.model.tenantCode = this.App.UseState.tenantCode;
            this.OnReady();
        })
    }

    OnReady() {

    }

    getTenantId(): number | undefined {
        if (this.model.tenantCode) {
            var ten = this.tenants.find(d => d.code == this.model.tenantCode);
            if (ten)
                return ten.id;
        }
        return undefined;
    }
}