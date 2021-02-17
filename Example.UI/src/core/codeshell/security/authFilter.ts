import { Injectable } from "@angular/core";
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router";
import { RouteData } from "./route-data";
import { AuthorizationServiceBase } from "./authorizationServiceBase";

@Injectable()
export class AuthFilter implements CanActivate {
    constructor(private authorizationService:AuthorizationServiceBase){

    }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Promise<boolean> {
        let data: RouteData = Object.assign(new RouteData, route.data);
        return this.authorizationService.IsAuthorizedAsync(data);
    }

}