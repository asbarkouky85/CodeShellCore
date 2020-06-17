import { Route } from "@angular/router";

import { AuthFilter } from "codeshell/security";
import { DomainData, ResourceActions } from "codeshell/security";
import { Translator } from "codeshell/localization";

import { Login } from "BaseApp/Main/Login";


import { ar_Loader } from "./Localization/ar/Loader";
import { en_Loader } from "./Localization/en/Loader";


var routes: Route[] = [
    { path: 'Login', component: Login, data: { action: 'anonymous', name:"Login" } }
	,
{ path:"Auth", loadChildren:"./Auth/AuthModule#AuthModule" },
	{ path:"Testing", loadChildren:"./Testing/TestingModule#TestingModule" },
	
    { path: '**', redirectTo: '/' }

];

Translator.SetLoaders({
    ["ar"]:new ar_Loader, ["en"]:new en_Loader, 
});


let data: DomainData[] | null = null;

export function GetDomainsData(): DomainData[] {
    if (!data) {
        data = [
			{
				name: "Main" ,
				children: [
					{ name: "Roles__RoleList", navigate: true, resource:"", action: "allowAll", apps: null , url: "Auth/Roles/RoleList"},
					{ name: "Testing__TestPage", navigate: true, resource:"", action: "allowAll", apps: null , url: "Testing/TestPage"},
					{ name: "Users__UserList", navigate: true, resource:"", action: "allowAll", apps: null , url: "Auth/Users/UserList"},]
			}
		];
    }
    return data;
}

export function Routes(): Route[] { return routes; }