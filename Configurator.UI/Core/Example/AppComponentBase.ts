import { Injector, ViewChild, EventEmitter } from "@angular/core";
import { Title } from "@angular/platform-browser";
import { ServerConfigBase, Shell } from "codeshell/core";
import { IAppComponent } from "codeshell/baseComponents";
import { TestLoader } from "codeshell/core";
import { ServerConfig, AppInfo } from "./ServerConfig";
import { UserDTO } from "codeshell/security";
import { Stored } from "codeshell/helpers";
import { UseState } from "./Models";



export class AppComponentBase extends IAppComponent {

    OnAppChanged: EventEmitter<AppInfo> = new EventEmitter<AppInfo>();
    UseState: UseState = new UseState;

    @ViewChild(TestLoader)
    ModalLoader?: TestLoader | undefined;
    constructor(inj: Injector, trans: Title, conf: ServerConfigBase) {
        super(inj, trans, conf);
        var apps = (Shell.Injector.get(ServerConfigBase) as ServerConfig).Apps;

        var state = Stored.Get("use_state", UseState);

        if (state && state.appName) {
            this.UseState = state;
            var info = apps.find(d => d.Name == this.UseState.appName);
            if (info)
                ServerConfig.CurrentApp = info;
        } else if (apps.length > 0) {
            ServerConfig.CurrentApp = apps[0];
            this.UseState.appName = ServerConfig.CurrentApp.Name;
            this.SaveState();
        }

        this.OnAppChanged.subscribe((d: AppInfo) => {
            this.UseState.appName = d.Name;
            this.UseState.tenantCode = undefined;
            this.SaveState();
            location.reload();
        });
        
    }

    OnStartupNoSession(res: any) {
        this.Router.navigateByUrl("/Login");
    }

    OnStartupSessionFound(user: UserDTO) {
        if (this.topBar) {
            this.topBar.user = user;
            this.topBar.isLoggedIn = true;
        }
           
    }

    SaveState() {
        Stored.Set("use_state", this.UseState);
    }
}