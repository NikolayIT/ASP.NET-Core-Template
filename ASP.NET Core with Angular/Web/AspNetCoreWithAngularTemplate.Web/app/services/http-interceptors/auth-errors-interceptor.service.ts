import { Injectable, Injector } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';

import { tap } from 'rxjs/operators/tap';
import { Observable } from 'rxjs/Observable';

import { IdentityService } from '../identity.service';

import { STATUS_CODES } from '../../app.constants';

@Injectable()
export class AuthErrorsInterceptorService implements HttpInterceptor {
    constructor(private injector: Injector, private router: Router) { }
    
    public intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(request).pipe(tap(
            () => { },
            (err: any) => {
                if (err instanceof HttpErrorResponse) {
                    const identityService = this.injector.get(IdentityService);

                    if (err.status === STATUS_CODES.UNAUTHORIZED) {
                        identityService.removeIdentity();
                        this.router.navigateByUrl('/account/login');
                    } else if (err.status === STATUS_CODES.FORBIDDEN) {
                        this.router.navigateByUrl('/');
                    }
                }
            }));
    }
}
