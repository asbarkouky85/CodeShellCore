import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { BaseModule } from 'codeshell';
import { ServerConfigBase } from "codeshell/core";
import { DomainDataProvider } from 'codeshell/security';

import { AppComponent } from './AppComponent';
import { ServerConfig } from "ExampleProject/ServerConfig";

import { SharedModule } from './Shared/SharedModule';

import { Routes, GetDomainsData } from './MainAppRoutes';

@NgModule({
    bootstrap: [AppComponent],
	declarations: [AppComponent,],
    imports: [
		SharedModule,
		
        RouterModule.forRoot(Routes())
    ],
    providers: [
        { provide: DomainDataProvider, useValue: new DomainDataProvider(GetDomainsData()) }
    ]
})
export class MainAppModule extends BaseModule {
	
}


