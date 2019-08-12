import { Route } from "@angular/router";

import { DomainData, ResourceActions } from "codeshell/security";

import { Login } from "ExampleProject/Main/Login";
import { Translator } from "codeshell/localization";

import { ar_Loader } from "./Localization/ar/Loader";
import { en_Loader } from "./Localization/en/Loader";


var routes: Route[] = [
    { path: 'Login', component: Login, data: { action: 'anonymous' } }
	,
{ path:"Auth", loadChildren:"./AuthModule#AuthModule" },
	
    { path: '**', redirectTo: '/' }

];

Translator.SetLoaders({
    ["ar"]:new ar_Loader, ["en"]:new en_Loader, 
});


let data: DomainData[] | null = null;

export function GetDomainsData(): DomainData[] {
    if (!data) {
        data = [
			{ name : "Auth" ,children: [
				{ name : "Auth__UserCreate", navigate: true, resource:"Auth__Users", action: ResourceActions.insert, apps: ["Admin"] },]},

		];
    }
    return data;
}

export function Routes(): Route[] { return routes; }