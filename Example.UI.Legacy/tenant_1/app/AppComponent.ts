import { Component, Injector } from '@angular/core';
import { trigger, state, style, transition, animate } from "@angular/animations";
import { Title } from '@angular/platform-browser';

import { ServerConfigBase,Shell } from 'codeshell/core';
import { AppComponentBase } from 'Example/AppComponentBase';

@Component({
    selector: 'app',
    templateUrl: './AppComponent.html',
    animations: [trigger("loader", [
        state("shown", style({ opacity: 1, visibility: 'visible' })),
        state("hidden", style({ opacity: 0, visibility: 'hidden' })),
        transition('shown => hidden', [animate('.3s')]),
        //transition('hidden => shown', [animate('1s')])
    ])]
})
export class AppComponent extends AppComponentBase {

    constructor(inj: Injector, trans: Title, conf: ServerConfigBase) {
        super(inj, trans,conf);
        Shell.Main = this;
    }
}
