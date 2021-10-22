import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AuthFilter, ResourceActions } from "codeshell/security";
import { DomainData } from 'codeshell/moldster';
import { Translator } from "codeshell/localization";

import { Login } from "@base/main/login.component";


import { ar_Loader } from "./localization/ar/loader";
import { en_Loader } from "./localization/en/loader";


Translator.SetLoaders({
    ["ar"]:new ar_Loader, ["en"]:new en_Loader, 
});

const routes: Routes = [
    { path: 'login', component: Login, data: { action: 'anonymous', name:"Login" } },
    { 
        path: 'shared', 
        loadChildren: () => import('./shared/shared.module').then(m => m.SharedModule) 
    },    { 
        path: 'generations', 
        loadChildren: () => import('./generations/generations.module').then(m => m.GenerationsModule) 
    },    { 
        path: 'routing', 
        loadChildren: () => import('./routing/routing.module').then(m => m.RoutingModule) 
    },    { 
        path: 'razor', 
        loadChildren: () => import('./razor/razor.module').then(m => m.RazorModule) 
    },    { 
        path: 'integration', 
        loadChildren: () => import('./integration/integration.module').then(m => m.IntegrationModule) 
    },    { 
        path: 'localization', 
        loadChildren: () => import('./localization/localization.module').then(m => m.LocalizationModule) 
    },
    
    { path: '**', redirectTo: '/' }

];

@NgModule({
  imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
  exports: [RouterModule],
})
export class AppRoutingModule {}

let data: DomainData[] | null = null;

export function GetDomainsData(): DomainData[] {
    if (!data) {
        data = [
			{
				name: "Main" ,
				children: [
					{ name: "Tenants__TenantList", navigate: true, resource:"", action: "anonymous", apps: null , url: "generations/tenants/tenant-list"},
					{ name: "Environments__EnvironmentList", navigate: true, resource:"", action: "allowAll", apps: null , url: "generations/environments/environment-list"},
					{ name: "Pages__PageTreeList", navigate: true, resource:"Pages", action: "anonymous", apps: null , url: "routing/pages/page-tree-list"},
					{ name: "CustomTexts__CustomTextList", navigate: true, resource:"", action: "allowAll", apps: null , url: "localization/custom-texts/custom-text-list"},
					{ name: "NavigationPages__NavGroupPages", navigate: true, resource:"NavigationGroups", action: "anonymous", apps: null , url: "routing/navigation-pages/nav-group-pages"},
					{ name: "Templates__PageCategoriesTreeList", navigate: true, resource:"PageCategoriesTreeList", action: "anonymous", apps: null , url: "razor/templates/page-categories-tree-list"},
					{ name: "Resources__ResourceList", navigate: true, resource:"", action: "allowAll", apps: null , url: "integration/resources/resource-list"},
					{ name: "Development__DevelopmentPanel", navigate: true, resource:"Generations", action: "anonymous", apps: null , url: "generations/development/development-panel"},
					{ name: "Parameters__PageReferenceList", navigate: true, resource:"", action: "anonymous", apps: null , url: "routing/parameters/page-reference-list"},]
			}
		];
    }
    return data;
}
