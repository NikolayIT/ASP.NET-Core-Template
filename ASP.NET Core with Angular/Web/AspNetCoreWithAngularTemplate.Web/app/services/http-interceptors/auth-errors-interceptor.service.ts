import { Injectable } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpErrorResponse } from '@angular/common/http';

import 'rxjs/add/operator/do';
import { Observable } from 'rxjs/Observable';

import { RouterService } from '../router.service';

import { STATUS_CODES } from '../../app.constants';

@Injectable()
export class AuthErrorsInterceptorService implements HttpInterceptor {
    constructor(private routerService: RouterService) { }

    public intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(request).do(
            () => { },
            (err: any) => {
                if (err instanceof HttpErrorResponse) {
                    if (err.status === STATUS_CODES.UNAUTHORIZED) {
                        this.routerService.redirectToLogin();
                    } else if (err.status === STATUS_CODES.FORBIDDEN) {
                        this.routerService.redirectToHome();
                    }
                }
            });
    }
}
