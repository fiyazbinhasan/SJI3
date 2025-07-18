import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './app/app.module';
import { environment } from './environments/environment';

export function getBaseUrl() {
  return document.getElementsByTagName('base')[0].href;
}

export function getPortalApiUrl() {
  return environment.portalApiUrl;
}

export function getSignalRHubUrl() {
  return environment.signalRHubUrl;
}

const providers = [
  { provide: 'BASE_URL', useFactory: getBaseUrl, deps: [] },
  { provide: 'PORTAL_API_URL', useFactory: getPortalApiUrl, deps: [] },
  { provide: 'SIGNALR_HUB_URL', useFactory: getSignalRHubUrl, deps: [] },
];

if (environment.production) {
  enableProdMode();
}

platformBrowserDynamic(providers)
  .bootstrapModule(AppModule)
  .catch((err) => console.log(err));
