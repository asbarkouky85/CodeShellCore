import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './tenant_1/app.module';
import { ServerConfig } from '@base/server-config';
import { initializeShell } from 'codeshell';

var prod=(new ServerConfig()).Production;
if (prod) {
  enableProdMode();
}

initializeShell();

platformBrowserDynamic().bootstrapModule(AppModule)
  .catch(err => console.error(err));
