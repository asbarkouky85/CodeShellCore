import { Environment } from '@abp/ng.core';

const baseUrl = 'http://196.202.126.106:8028';

export const environment = {
  production: true,
  application: {
    baseUrl,
    name: 'Cms',
  },
  oAuthConfig: {
    issuer: 'http://196.202.126.106:8027',
    redirectUri: baseUrl,
    clientId: 'Cms_App',
    responseType: 'code',
    scope: 'offline_access Cms'
  },
  apis: {
    default: {
      url: 'http://196.202.126.106:8027',
      rootNamespace: 'Sotk.Cms',
    },
  },
} as Environment;
