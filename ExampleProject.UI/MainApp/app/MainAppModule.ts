import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';

import { BaseModule, CodeShellModule } from 'codeshell';
import { DomainDataProvider } from 'codeshell/security';

import { ExampleProjectBaseModule } from 'ExampleProject/ExampleProjectBaseModule';

import { AppComponent } from './AppComponent';
import { SharedModule } from './Shared/SharedModule';
import { Routes, GetDomainsData } from './MainAppRoutes';
import { CommonModule } from '@angular/common';


@NgModule({
    bootstrap: [AppComponent],
    declarations: [AppComponent,],
    imports: [
        ToastrModule.forRoot(),
        CodeShellModule.forRoot(),
        ExampleProjectBaseModule.forRoot(),
        SharedModule,
        BrowserModule,
        BrowserAnimationsModule,
        RouterModule.forRoot(Routes())
    ],
    providers: [
        { provide: DomainDataProvider, useValue: new DomainDataProvider(GetDomainsData()) }
    ]
})
export class MainAppModule {//extends BaseModule {
	
}


