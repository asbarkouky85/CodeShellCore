import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';

import { CodeShellModule } from 'codeshell/codeShellModule';
import { BaseModule } from "codeshell/baseModule";
import { DomainDataProvider } from 'codeshell/security';

import { ExampleBaseModule } from 'Example/ExampleBaseModule';

import { AppComponent } from './AppComponent';
import { SharedModule } from './Shared/SharedModule';
import { Routes, GetDomainsData } from './ClientAppRoutes';


@NgModule({
    bootstrap: [AppComponent],
	declarations: [AppComponent,],
    imports: [
		ToastrModule.forRoot(),
        CodeShellModule.forRoot(),
        ExampleBaseModule.forRoot(),
        SharedModule,
        BrowserModule,
        BrowserAnimationsModule,
        RouterModule.forRoot(Routes())
    ],
    providers: [
        { provide: DomainDataProvider, useValue: new DomainDataProvider(GetDomainsData()) }
    ]
})
export class ClientAppModule extends BaseModule {
	
}


