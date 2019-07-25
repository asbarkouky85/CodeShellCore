import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { BaseModule } from 'CodeShell/BaseModule';
import { IServerConfig } from "CodeShell/IServerConfig";
import { DomainDataProvider } from 'CodeShell/Security/Models';

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
        { provide: DomainDataProvider, useValue: new DomainDataProvider(GetDomainsData()) },
        { provide: IServerConfig, useValue: ServerConfig.Instance },
    ]
})
export class MainAppModule extends BaseModule {
	
}


