import { Route } from "@angular/router";

import { AuthFilter } from "CodeShell/Security/AuthFilter";
import { DomainData, ResourceActions } from "CodeShell/Security/Models";
import { Translator } from "CodeShell/Localization/Translator";

import { Login } from "ExampleProject/Main/Login";


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
			{ name : "Auth" ,children: []},

		];
    }
    return data;
}

export function Routes(): Route[] { return routes; }