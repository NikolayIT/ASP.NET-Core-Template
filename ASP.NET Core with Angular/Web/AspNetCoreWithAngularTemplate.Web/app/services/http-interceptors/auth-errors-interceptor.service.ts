import { Injectable } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpErrorResponse } from '@angular/common/http';

import { tap } from 'rxjs/operators/tap';
import { Observable } from 'rxjs/Observable';

import { IdentityService } from '../identity.service';
import { RouterService } from '../router.service';

import { STATUS_CODES } from '../../app.constants';

@Injectable()
export class AuthErrorsInterceptorService implements HttpInterceptor {
    constructor(private identityService: IdentityService, private routerService: RouterService) { }

    public intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(request).pipe(tap(
            () => { },
            (err: any) => {
                if (err instanceof HttpErrorResponse) {
                    if (err.status === STATUS_CODES.UNAUTHORIZED) {
                        this.identityService.removeIdentity();
                        this.routerService.redirectToLogin();
                    } else if (err.status === STATUS_CODES.FORBIDDEN) {
                        this.routerService.redirectToHome();
                    }
                }
            }));
    }
}
