﻿import { Shell, ServerConfigBase } from "codeshell/core";
import { Router } from "@angular/router";
import { HttpClient } from "@angular/common/http";
import { UserDTO } from "codeshell/security";

export class TopBarBase {
    isLoggedIn: boolean = false;
    navState: boolean = true;
    changePasswordItem: boolean = false;
    editProfileItem: boolean = false;
    user?: UserDTO;
    Lang?: string;

    ngOnInit() {
        Shell.Session.GetUserAsync().then(d => {
            this.isLoggedIn = true;
            this.user = d;
            this.OnSession(this.user);
            this.OnReady();
        }).catch(d => {
            this.OnStartNoSession();
            this.OnReady();
        });

        Shell.Session.LogStatus.subscribe((v: boolean) => {
            this.isLoggedIn = v;
            this.OnLogStatusChange(v);
        });

        Shell.Session.OnLogin.subscribe((u: UserDTO) => {
            this.user = u
            this.OnSession(u);
        });
        Shell.ViewLoaded.subscribe((d: any) => {
            $(".wrapper-side").removeClass("expanded");
        });

        var conf: ServerConfigBase = Shell.Injector.get(ServerConfigBase);
        this.Lang = conf.Locale;
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
    OnSession(userDto: UserDTO) { }

    /**
     * overridable : called when failing to obtain user data on startup
     */
    OnStartNoSession() { }

    Logout() {
        Shell.Session.EndSession();
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
        var cl: HttpClient = Shell.Injector.get(HttpClient);
        var conf: ServerConfigBase = Shell.Injector.get(ServerConfigBase);
        cl.get("/Home/SetLocale/?lang=" + (conf.Locale == 'ar' ? 'en' : 'ar')).subscribe(d => {
            location.reload();
        });
    }

    SetLang(lng:string) {
        var cl: HttpClient = Shell.Injector.get(HttpClient);
        var conf: ServerConfigBase = Shell.Injector.get(ServerConfigBase);
        this.Lang = conf.Locale;
        cl.get("/Home/SetLocale/?lang=" + lng).subscribe(d => {
            this.Lang = lng;
            location.reload();
        });
    }
    ChangePassword() {

    }

    EditProfile() {

    }

}