import { Route } from "@angular/router";

import { AuthFilter } from "codeshell/security";
import { DomainData, ResourceActions } from "codeshell/security";
import { Translator } from "codeshell/localization";

import { Login } from "Example/Main/Login";


import { ar_Loader } from "./Localization/ar/Loader";
import { en_Loader } from "./Localization/en/Loader";


var routes: Route[] = [
    { path: 'Login', component: Login, data: { action: 'anonymous', name:"Login" } }
	,
{ path:"Tenants", loadChildren:"./Tenants/TenantsModule#TenantsModule" },
	{ path:"Domains", loadChildren:"./Domains/DomainsModule#DomainsModule" },
	{ path:"Pages", loadChildren:"./Pages/PagesModule#PagesModule" },
	{ path:"PageCategories", loadChildren:"./PageCategories/PageCategoriesModule#PageCategoriesModule" },
	{ path:"NavigationGroups", loadChildren:"./NavigationGroups/NavigationGroupsModule#NavigationGroupsModule" },
	{ path:"PageControls", loadChildren:"./PageControls/PageControlsModule#PageControlsModule" },
	{ path:"Generations", loadChildren:"./Generations/GenerationsModule#GenerationsModule" },
	{ path:"Resources", loadChildren:"./Resources/ResourcesModule#ResourcesModule" },
	
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
					{ name: "Tenants__TenantList", navigate: true, resource:"", action: "anonymous", apps: null , url: "Tenants/TenantList"},
					{ name: "Pages__PageTreeList", navigate: true, resource:"Pages", action: "anonymous", apps: null , url: "Pages/PageTreeList"},
					{ name: "NavigationGroups__NavGroupPages", navigate: true, resource:"NavigationGroups", action: "anonymous", apps: null , url: "NavigationGroups/NavGroupPages"},
					{ name: "PageCategories__PageCategoriesTreeList", navigate: true, resource:"PageCategoriesTreeList", action: "anonymous", apps: null , url: "PageCategories/PageCategoriesTreeList"},
					{ name: "Resources__ResourceList", navigate: true, resource:"", action: "allowAll", apps: null , url: "Resources/ResourceList"},
					{ name: "Generations__GenerationNotification", navigate: true, resource:"Generations", action: "anonymous", apps: null , url: "Generations/GenerationNotification"},]
			}
		];
    }
    return data;
}

export function Routes(): Route[] { return routes; }