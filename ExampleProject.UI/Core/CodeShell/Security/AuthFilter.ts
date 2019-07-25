import { Injectable } from "@angular/core";
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Route } from "@angular/router";
import { SessionManager } from "CodeShell/Security/SessionManager";
import { Observable } from "rxjs/Observable";
import { UserDTO, RouteData } from "CodeShell/Security/Models";
import { Shell } from "CodeShell/Shell";

@Injectable()
export class AuthFilter implements CanActivate {


    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | Observable<boolean> | Promise<boolean> {
       //debugger;
        let Mananger: SessionManager = SessionManager.Current();

        let data: RouteData = Object.assign(new RouteData, route.data);

        if (data.IsAnonymous) {
            Shell.Main.Public = true;
            return true;
        }
        
        if (Mananger.GetToken() == null)
            return false;

        return Mananger.IsAuthorized(data);
    }

}