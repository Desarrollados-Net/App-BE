import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

const oAuthConfig = {
  issuer: 'https://localhost:44366/',
  redirectUri: baseUrl,
  clientId: 'SeriesPlus_App',
  responseType: 'code',
  scope: 'offline_access SeriesPlus',
  requireHttps: true,
};

export const environment = {
  production: true,
  application: {
    baseUrl,
    name: 'SeriesPlus',
  },
  oAuthConfig,
  apis: {
    default: {
      url: 'https://localhost:44366',
      rootNamespace: 'SeriesPlus',
    },
    AbpAccountPublic: {
      url: oAuthConfig.issuer,
      rootNamespace: 'AbpAccountPublic',
    },
  },
  remoteEnv: {
    url: '/getEnvConfig',
    mergeStrategy: 'deepmerge'
  }
} as Environment;
