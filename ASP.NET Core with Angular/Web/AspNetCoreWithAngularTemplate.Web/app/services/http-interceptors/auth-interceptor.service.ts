import { Injectable } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpHeaders } from '@angular/common/http';

import { Observable } from 'rxjs/Observable';

import { IdentityService } from '../identity.service';

@Injectable()
export class AuthInterceptorService implements HttpInterceptor {
    constructor(private identityService: IdentityService) { }

    public intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const authToken = this.identityService.getToken();

        const authRequest = request.clone({ setHeaders: { Authorization: authToken } });

        return next.handle(authRequest);
    }
}
