import { Component, Injector, ViewChild, trigger, state, style, transition, animate } from '@angular/core';
import { Title } from '@angular/platform-browser';

import { ServerConfigBase,Shell } from 'codeshell/core';
import { AppComponentBase } from 'ExampleProject/AppComponentBase';

@Component({
    selector: 'app',
    templateUrl: './AppComponent.html',
    animations: [trigger("loader", [
        state("shown", style({ opacity: 1, visibility: 'visible' })),
        state("hidden", style({ opacity: 0, visibility: 'hidden' })),
        transition('shown => hidden', [animate('.8s')]),
        //transition('hidden => shown', [animate('1s')])
    ])]
})
export class AppComponent extends AppComponentBase {

    constructor(inj: Injector, trans: Title, conf: ServerConfigBase) {
        super(inj, trans,conf);
        Shell.Main = this;
    }
}
