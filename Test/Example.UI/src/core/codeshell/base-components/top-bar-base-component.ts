import { ServerConfigBase } from "codeshell/serverConfigBase";
import { Shell } from "codeshell/shell";
import { Router } from "@angular/router";
import { HttpClient } from "@angular/common/http";
import { SessionManager, UserDTO } from "codeshell/security";
import { BaseComponent } from "./base-component";
import { EditComponentBase } from "./edit-base-component";
import { Component, ComponentRef } from "@angular/core";
import { NotificationListenerBase } from "codeshell/http/notificationListenerBase";
import { NotificationServiceBase } from "codeshell/http/notificationServiceBase";
import { from } from 'rxjs';
import { TranslateService } from '@ngx-translate/core';
import { Culture } from "codeshell/localization/locale-data";


@Component({ template: '' })
export class TopBarBase {
    isLoggedIn: boolean = false;
    navState: boolean = true;
    changePasswordItem: boolean = false;
    editProfileItem: boolean = false;
    notificationCount: number = 0;
    user?: UserDTO;
    Lang?: string;
    changePasswordComponent?: string;
    editProfileComponent?: string;
    Listener?: NotificationListenerBase;
    NotificationService?: NotificationServiceBase;

    get Router(): Router {
        return Shell.Injector.get(Router);
    }

    constructor() {

    }

    private _startListener() {
        if (this.Listener) {
            this.Listener.NotificationsChanged.subscribe(e => {
                console.log(e);
                this.notificationCount = e;
                this.OnNotificationChanged(e);
            });
            this.Listener.KeepAlive = true;
        }
    }

    ngOnInit() {

        this._startListener();
        SessionManager.Current.GetUserAsync().then(d => {
            this.isLoggedIn = true;
            this.user = d;
            this._onUser(this.user);
            this.OnReady();
        }).catch(d => {
            this.OnStartNoSession();
            this.OnReady();
        });

        SessionManager.Current.LogStatus.subscribe((v: boolean) => {
            this.isLoggedIn = v;
            this.OnLogStatusChange(v);
        });

        SessionManager.Current.OnLogin.subscribe((u: UserDTO) => {
            this.user = u;
            this._onUser(u);
        });
        Shell.ViewLoaded.subscribe((d: any) => {
            //$(".wrapper-side").removeClass("expanded");
        });

        var conf: ServerConfigBase = Shell.Injector.get(ServerConfigBase);
        this.Lang = Culture.Current.Language;
    }

    /**
     * overridable : called after user data is obtained from server
     */
    OnReady() { }

    /**
     * overridable called when user logs in or out
     * @param status
     */
    OnLogStatusChange(status: boolean) { }

    /**
     * called after login or when user is found
     */
    OnSession(userDto: UserDTO) {

    }

    private _onUser(dto: UserDTO) {
        if (this.Listener) {
            this.Listener.StartWithUser(dto.userId);
        }

        if (this.NotificationService)
            this.NotificationService.GetCount().then(e => this.notificationCount = e);
        this.OnSession(dto);
    }

    OnNotificationChanged(c: number) {

    }

    /**
     * overridable : called when failing to obtain user data on startup
     */
    OnStartNoSession() { }

    Logout() {
        SessionManager.Current.EndSession();
        this.isLoggedIn = false;
        Shell.Injector.get(Router).navigateByUrl("/Login");
    }

    Slide() {
        if (!Shell.Main.ShowNav)
            return;
        $(".wrapper-side").toggleClass("expanded");
    }

    ToggleNav() {
        if (!Shell.Main.ShowNav)
            return;
        this.setSideBarState(!this.navState);
    }

    setSideBarState(state: boolean) {
        this.navState = state;
        if (!state) {
            $(".wrapper-side").addClass("compressed");
            $(".wrapper-content").addClass("expanded");
        } else {
            $(".wrapper-side").removeClass("compressed");
            $(".wrapper-content").removeClass("expanded");
        }
    }

    ChangeLang() {
        var conf: TranslateService = Shell.Injector.get(TranslateService);
        var toLang = conf.currentLang == "ar" ? "en" : "ar";
        Shell.Main.ChangeLangAsync(toLang);
    }

    SetLang(toLang: string) {
        Shell.Main.ChangeLangAsync(toLang);
    }

    protected OpenModal<T extends BaseComponent>(path: string): Promise<ComponentRef<T>> {
        var comp = Shell.Main.GetDialogAs<T>(path);
        if (comp != null) {
            return Promise.resolve(comp);
        }
        return Promise.reject("not found");
    }

    ChangePassword() {
        if (this.changePasswordComponent) {
            this.OpenModal<EditComponentBase>(this.changePasswordComponent).then(comp => {
                comp.instance.Show = true;
                comp.instance.DataSubmitted = res => {
                    comp.instance.Show = false;
                }
            })
        }

    }

    EditProfile() {
        if (this.editProfileComponent)
            this.Router.navigateByUrl(this.editProfileComponent);
    }



}