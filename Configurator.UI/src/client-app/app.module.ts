import { NgModule } from '@angular/core';
import { APP_BASE_HREF } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';

import { CodeShellModule } from 'codeshell';
import { DomainDataProvider } from 'codeshell/moldster';

import { ExampleBaseModule } from '@base/example-base.module';

import { AppComponent } from './app.component';
import { SharedModule } from './shared/shared.module';
import { AppRoutingModule, GetDomainsData } from './app-routing.module';


@NgModule({
    bootstrap: [AppComponent],
	declarations: [AppComponent, ],
    imports: [
		ToastrModule.forRoot(),
        CodeShellModule.forRoot(),
        ExampleBaseModule.forRoot(),
        SharedModule,
        BrowserModule,
        BrowserAnimationsModule,
        AppRoutingModule
    ],
    providers: [
        { provide: DomainDataProvider, useValue: new DomainDataProvider(GetDomainsData()) },
        
    ]
})
export class AppModule {
	
}


