import { OpaqueToken } from '@angular/core';

export interface AppConfig {
    apiEndPoint: string;
}

export let APP_CONFIG = new OpaqueToken('AppConfig');
export const SPYSTORE_CONFIG: AppConfig = {
    apiEndPoint: 'http://localhost:40001/api/'
}

