import "@angular/compiler";

import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './client-app/app.module';
import { ServerConfig } from '@base/server-config';

var conf = new ServerConfig();

if (conf.Production) {
  enableProdMode();
}

platformBrowserDynamic().bootstrapModule(AppModule)
  .catch(err => console.error(err));
