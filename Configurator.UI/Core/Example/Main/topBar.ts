import { Component } from "@angular/core";
import { Shell, ServerConfigBase } from "codeshell/core";

import { BaseComponent } from "codeshell/baseComponents";
import { UserDTO } from "codeshell/security";
import { HttpClient } from "@angular/common/http";
import { ServerConfig, AppInfo } from "Example/ServerConfig";
import { AppComponentBase } from "Example/AppComponentBase";
import { Router } from "@angular/router";
import { TopBarBase } from "codeshell/baseComponents/topBarBase";
import { TasksListener } from "Example/Http/TasksListener";
import { stringify } from "@angular/compiler/src/util";
import { NoteType } from "codeshell/helpers";

@Component({ templateUrl: "./topBar.html", selector: "top-bar", exportAs: "top-bar" })
export class TopBar extends TopBarBase {

    apps: AppInfo[] = [];
    currentApp: AppInfo | null = null;
    Tasks = new TasksListener(true);
    sub: any = null;

    ngOnInit() {
        super.ngOnInit();
        this.apps = (Shell.Injector.get(ServerConfigBase) as ServerConfig).Apps;
        this.currentApp = ServerConfig.CurrentApp;
       
        this.Tasks.TaskChanged.subscribe(d => {
            var n: { [key: string]: string } = {};
            n.tenantCode = d.tenantCode as string;
            n.version = d.version as string;
            var mess = Shell.Message(d.message as string, n);
            Shell.Main.ShowPrompt(mess);
        });
        if (!this.sub) {
            this.sub = this.Tasks.OnClosed.subscribe(d => {
                setTimeout(() => {
                    this.Tasks.Start();
                }, 4000)
            })
        }

        this.Tasks.Start();
    }

    appChanged() {
        if (this.currentApp) {
            ServerConfig.CurrentApp = this.currentApp;
            (Shell.Main as AppComponentBase).OnAppChanged.emit(this.currentApp);
        }

    }
}