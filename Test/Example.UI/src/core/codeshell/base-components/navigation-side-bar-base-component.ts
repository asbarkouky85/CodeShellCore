import { UserDTO, AuthorizationServiceBase, RouteData, SessionManager } from "codeshell/security";
import { Shell } from "codeshell/shell";
import { Component, Injector } from "@angular/core";
import { Router } from "@angular/router";
import { absUrl } from 'codeshell/utilities';
import { DomainDataProvider, FunctionItem } from 'codeshell/moldster';


@Component({ template: '' })
export class NavigationSideBarBase {
    user?: UserDTO;
    isLoggedIn: boolean = false;
    navs: FunctionItem[] = [];
    get Router(): Router { return this.Injector.get(Router); }

    constructor(protected Injector: Injector) {

    }

    ngOnInit() {

        SessionManager.Current.LogStatus.subscribe((v: boolean) => {
            this.isLoggedIn = v;
            this.LoadNavigation();
        });
        SessionManager.Current.GetUserAsync()
            .then(user => {

                this.user = user;
                this.isLoggedIn = true;
                this.LoadNavigation();
            })
            .catch(d => {
                this.LoadNavigation();
            });
        this.OnReady();
    }

    OnReady() { }

    GetMainUrl(): string {
        return "/";
    }

    GotoMain() {
        var main = absUrl(this.GetMainUrl());
        console.log(main);
        this.Router.navigateByUrl(main);
    }

    LoadNavigation() {
        var auth: AuthorizationServiceBase = Shell.Injector.get(AuthorizationServiceBase);
        var doms: DomainDataProvider = Shell.Injector.get(DomainDataProvider);
        for (var dom of doms.Domains) {

            if (dom.name == "Main") {
                for (var c of dom.children) {
                    var r: RouteData = Object.assign(new RouteData, c);
                    if (auth.IsAuthorized(this.user, r) && r.url) {
                        var item: FunctionItem = {
                            name: r.name,
                            url: r.url
                        }
                        this.navs.push(item);
                    }
                }
            }
        }
    }

    Logout() {
        SessionManager.Current.EndSession();
        location.pathname = "/";
    }
}