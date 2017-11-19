import { Injectable, Injector } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpErrorResponse } from '@angular/common/http';

import { tap } from 'rxjs/operators/tap';
import { Observable } from 'rxjs/Observable';

import { IdentityService } from '../identity.service';
import { RouterService } from '../router.service';

import { STATUS_CODES } from '../../app.constants';

@Injectable()
export class AuthErrorsInterceptorService implements HttpInterceptor {
    constructor(private injector: Injector) { }

    public intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(request).pipe(tap(
            () => { },
            (err: any) => {
                if (err instanceof HttpErrorResponse) {
                    const identityService = this.injector.get(IdentityService);
                    const routerService = this.injector.get(RouterService);

                    if (err.status === STATUS_CODES.UNAUTHORIZED) {
                        identityService.removeIdentity();
                        routerService.redirectToLogin();
                    } else if (err.status === STATUS_CODES.FORBIDDEN) {
                        routerService.redirectToHome();
                    }
                }
            }));
    }
}
