import { TodoItemsDataService } from './data/todo-items-data.service';

import { AuthErrorsInterceptorService } from './http-interceptors/auth-errors-interceptor.service';
import { AuthInterceptorService } from './http-interceptors/auth-interceptor.service';

import { AuthService } from './auth.service';
import { IdentityService } from './identity.service';
import { LoggerService } from './logger.service';
import { StorageService } from './storage.service';

export * from './data/todo-items-data.service';

export * from './http-interceptors/auth-errors-interceptor.service';
export * from './http-interceptors/auth-interceptor.service';

export * from './auth.service';
export * from './identity.service';
export * from './logger.service';
export * from './storage.service';

export const APP_SERVICES = [
    TodoItemsDataService,

    AuthErrorsInterceptorService,
    AuthInterceptorService,

    AuthService,
    IdentityService,
    LoggerService,
    StorageService
];
