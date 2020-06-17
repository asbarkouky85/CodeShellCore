import 'reflect-metadata';
import 'zone.js';
import 'bootstrap';
import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { ClientAppModule } from './app/ClientAppModule';


if (module.hot) {

	module.hot.addStatusHandler(status => {
        if (status == "idle") {
            var queryString = '?reload=' + new Date().getTime();
            var style = document.getElementById("app-style") as HTMLLinkElement;
            if (style) {
                style.href = style.href.replace(/\?.*|$/, queryString);
                console.log("[HMR] Reloaded stylesheet : " + style.href)
            }
        }
    });

    module.hot.accept();

    module.hot.dispose(() => {
        // Before restarting the app, we create a new root element and dispose the old one
        const oldRootElem = document.querySelector('app');
        const newRootElem = document.createElement('app');
        oldRootElem!.parentNode!.insertBefore(newRootElem, oldRootElem);
		oldRootElem!.remove();
        modulePromise.then(appModule => appModule.destroy());
    });

} else {
    enableProdMode();
}

// Note: @ng-tools/webpack looks for the following expression when performing production
// builds. Don't change how this line looks, otherwise you may break tree-shaking.
const modulePromise = platformBrowserDynamic().bootstrapModule(ClientAppModule);
