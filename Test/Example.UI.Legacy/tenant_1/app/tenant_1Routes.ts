import { Route } from "@angular/router";

import { AuthFilter } from "codeshell/security";
import { DomainData, ResourceActions } from "codeshell/security";
import { Translator } from "codeshell/localization";

import { Login } from "Example/Main/Login";


import { ar_Loader } from "./../Localization/ar/loader";
import { en_Loader } from "./../Localization/en/loader";


var routes: Route[] = [
    { path: 'Login', component: Login, data: { action: 'anonymous', name:"Login" } }
	,
{ path:"Public", loadChildren:"./Public/PublicModule#PublicModule" },
	
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
					{ name: "HomeSlides__HomeSlideList", navigate: true, resource:"", action: "allowAll", apps: null , url: "public/HomeSlides/HomeSlideList"},]
			}
		];
    }
    return data;
}

export function Routes(): Route[] { return routes; }