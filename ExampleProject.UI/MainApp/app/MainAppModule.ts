import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { BaseModule } from 'codeshell';
import { ServerConfigBase } from "codeshell/core";
import { DomainDataProvider } from 'codeshell/security';

import { ExampleProjectBaseModule } from 'ExampleProject/ExampleProjectBaseModule';
import { AppComponent } from './AppComponent';
import { ServerConfig } from "ExampleProject/ServerConfig";

import { SharedModule } from './SharedModule';

import { Routes, GetDomainsData } from './MainAppRoutes';

@NgModule({
    bootstrap: [AppComponent],
	declarations: [AppComponent,],
    imports: [
        ExampleProjectBaseModule.forRoot(),
		SharedModule,
		
        RouterModule.forRoot(Routes())
    ],
    providers: [
        { provide: DomainDataProvider, useValue: new DomainDataProvider(GetDomainsData()) }
    ]
})
export class MainAppModule extends BaseModule {
	
}


