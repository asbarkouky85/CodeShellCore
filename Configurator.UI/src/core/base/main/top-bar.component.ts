import { Component } from "@angular/core";
import { AppComponentBase } from "@base/app-component-base.component";
import { TasksListener } from "@base/http/tasks-listener";
import { AppInfo, Tenant } from "@base/models";
import { ServerConfig } from "@base/server-config";
import { Shell, ServerConfigBase } from "codeshell";
import { TopBarBase } from "codeshell/base-components";

@Component({ templateUrl: "./top-bar.component.html", selector: "top-bar", exportAs: "top-bar" })
export class TopBar extends TopBarBase {

    apps: AppInfo[] = [];
    currentApp: AppInfo | null = null;
    tenants: Tenant[] = [];
    Tasks: TasksListener;
    sub: any = null;
    showUserMenu: boolean = false;

    get App(): AppComponentBase { return Shell.MainAs<AppComponentBase>(); }

    constructor() {
        super();


    }

    toggleMenu() {
        this.showUserMenu = !this.showUserMenu;
    }

    ngOnInit() {
        super.ngOnInit();

        this.App.getAppListAsync().then(res => {
            this.apps = res;
            this.currentApp = ServerConfig.CurrentApp;
            this.Tasks = new TasksListener(this.currentApp.configUrl);
            this.Tasks.KeepAlive = true;
            this.Tasks.TaskChanged.subscribe(d => {
                var n: { [key: string]: string } = {};
                n.tenantCode = d.tenantCode as string;
                n.version = d.version as string;
                var mess = Shell.Message(d.message as string, n.tenantCode, n.version);
                Shell.Main.ShowPrompt(mess);
            });
            this.Tasks.Start();
        })


    }

    appChanged() {
        if (this.currentApp) {
            ServerConfig.CurrentApp = this.currentApp;
            this.App.OnAppChanged.emit(this.currentApp);
        }
    }

    tenantChanged() {
        this.App.SaveState();
        var t = this.tenants.find(d => d.code == this.App.UseState.tenantCode);
        if (t)
            this.App.OnTenantChanged.emit(t);
        else
            this.App.OnTenantChanged.emit(undefined);
    }

    Definition() {
        var ten = this.App.UseState.tenantCode;
        this.App.GetTracer().then(tr => {
            if (ten) {
                tr.Definition(ten);
            }
        })
    }



    RenderAll() {
        var ten = this.App.UseState.tenantCode;

        this.App.GetTracer().then(tr => {
            if (ten) {
                tr.RenderAll(ten);
            }
        })

    }
}