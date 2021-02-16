import { UserDTO, AuthorizationServiceBase, DomainDataProvider, RouteData } from "codeshell/security";
import { FunctionItem } from "codeshell/security/navs";
import { Shell } from "codeshell/shell";
import { Component, Injector } from "@angular/core";
import { Router } from "@angular/router";
import { absUrl } from "codeshell/utilities/utils";


@Component({ template: '' })
export class NavigationSideBarBase {
    user?: UserDTO;
    isLoggedIn: boolean = false;
    navs: FunctionItem[] = [];
    get Router(): Router { return this.Injector.get(Router); }

    constructor(protected Injector: Injector) {

    }

    ngOnInit() {

        Shell.Session.LogStatus.subscribe((v: boolean) => {
            this.isLoggedIn = v;
            this.LoadNavigation();
        });
        Shell.Session.GetUserAsync()
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
        Shell.Session.EndSession();
        location.pathname = "/";
    }
}