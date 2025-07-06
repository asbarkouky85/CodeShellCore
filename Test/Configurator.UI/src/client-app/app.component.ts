import { Component, Injector } from '@angular/core';
import { Title } from '@angular/platform-browser';

import { Shell } from 'codeshell/main';
import { AppComponentBase } from '@base/app-component-base.component';

@Component({
    selector: 'app',
    templateUrl: './app.component.html'
})
export class AppComponent extends AppComponentBase {

    constructor(inj: Injector, trans: Title) {
        super(inj, trans);
        Shell.Main = this;
    }
}
