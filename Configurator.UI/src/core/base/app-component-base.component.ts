import { ViewChild, Injector, EventEmitter, Component } from "@angular/core";
import { Title } from "@angular/platform-browser";
import { ServerConfigBase, Shell } from "codeshell";
import { AppBaseComponent } from "codeshell/base-components";
import { ComponentLoader } from "codeshell/directives";
import { SessionManager, UserDTO } from "codeshell/security";
import { Stored } from "codeshell/services";
import { GenerationInofBase } from "./generations/generation-inof-base.component";
import { TenantsService } from "./http";
import { AccountService } from "./http/account.service";
import { AppInfo, Tenant, UseState } from "./models";
import { ServerConfig } from "./server-config";

@Component({ template: '' })
export class AppComponentBase extends AppBaseComponent {

    private _appListLoaded: boolean = false;
    AppList: AppInfo[] = [];
    TenantList: Tenant[] = [];

    ready: boolean = false;
    RedirectToLogin = true;
    accountService = new AccountService();

    OnAppChanged: EventEmitter<AppInfo> = new EventEmitter<AppInfo>();
    OnTenantChanged: EventEmitter<Tenant | undefined> = new EventEmitter<Tenant | undefined>();

    UseState: UseState = new UseState;

    private tenantService = new TenantsService();
    private _tracer?: GenerationInofBase;
    async GetTracer(): Promise<GenerationInofBase> {

        if (!this._tracer) {
            var m = await this.GetDialogAs<GenerationInofBase>("Shared/ServerTracerModal");
            if (m)
                this._tracer = m.instance;
        }
        return this._tracer as GenerationInofBase;
    }

    @ViewChild(ComponentLoader)
    ModalLoader?: ComponentLoader | undefined;

    constructor(inj: Injector, trans: Title) {
        super(inj, trans);
        this.OnAppChanged.subscribe((d: AppInfo) => this.onAppInfoChanged(d));
    }

    ngOnInit() {
        super.ngOnInit();

    }

    async getAppListAsync(): Promise<AppInfo[]> {
        if (!this._appListLoaded) {

            this.AppList = await this.accountService.GetApps();
            this._appListLoaded = true;
            var state = Stored.Get("use_state", UseState);
            if (state && state.appName) {
                this.UseState = state;
                var info = this.AppList.find(d => d.name == this.UseState.appName);
                if (info)
                    ServerConfig.CurrentApp = info;
            } else if (this.AppList.length > 0) {
                ServerConfig.CurrentApp = this.AppList[0];
                this.UseState.appName = ServerConfig.CurrentApp.name;
                this.SaveState();
            }
        }
        return this.AppList;

    }

    onAppInfoChanged(d: AppInfo | null) {
        this.UseState.appName = d.name;
        this.UseState.tenantCode = undefined;
        this.SaveState();
        location.reload();
    }

    async AppDataReady(): Promise<Tenant[]> {
        if (this.ready)
            return this.TenantList;
        else {
            await SessionManager.Current.GetUserAsync();
            this.AppList = await this.getAppListAsync();
            this.TenantList = await this.loadTenants()
            this.ready = true;
            return this.TenantList;
        }
    }

    async loadTenants(): Promise<Tenant[]> {
        if (ServerConfig.CurrentApp != null) {
            var res = await this.tenantService.GetPaged("Get", { Showing: 0, Skip: 0 });
            return res.list;
        } else {
            return [];
        }
    }

    GetTenantId(): number | undefined {
        var t = this.TenantList.find(d => d.code == this.UseState.tenantCode);
        if (t)
            return t.id;
        return undefined;
    }

    OnStartupNoSession(res: any) {
        this.Router.navigateByUrl("/login");
    }

    OnLogStatusChanged(st: boolean) {
        if (st)
            this.AppDataReady();
    }

    OnStartupSessionFound(user: UserDTO) {
        if (this.topBar) {
            this.topBar.user = user;
            this.topBar.isLoggedIn = true;
        }
        this.AppDataReady();
    }

    SaveState() {
        Stored.Set("use_state", this.UseState);
    }
}