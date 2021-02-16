import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AuthFilter, DomainData, ResourceActions } from "codeshell/security";
import { Translator } from "codeshell/localization";

import { Login } from "@base/main/login.component";




Translator.SetLoaders({
    
});

const routes: Routes = [
    { path: 'login', component: Login, data: { action: 'anonymous', name:"login" } },

    
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
			
		];
    }
    return data;
}
