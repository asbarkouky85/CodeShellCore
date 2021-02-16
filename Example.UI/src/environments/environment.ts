
const baseUrl = 'http://localhost:4200';

export const environment = {
  production: false,
  application: {
    baseUrl,
    name: 'Cms',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44380',
    redirectUri: baseUrl,
    clientId: 'Cms_App',
    responseType: 'code',
    scope: 'offline_access openid profile role email phone Cms',
  },
  apis: {
    default: {
      url: 'https://localhost:44380',
      rootNamespace: 'Sotk.Cms',
    },
  },
} ;
