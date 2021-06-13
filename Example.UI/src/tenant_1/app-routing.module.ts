﻿import { NgModule } from '@angular/core';
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
        path: 'public', 
        loadChildren: () => import('./public/public.module').then(m => m.publicModule) 
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
					{ name: "HomeSlides__HomeSlideList", navigate: true, resource:"", action: "allowAll", apps: null , url: "public/home-slides/home-slide-list"},]
			}
		];
    }
    return data;
}
