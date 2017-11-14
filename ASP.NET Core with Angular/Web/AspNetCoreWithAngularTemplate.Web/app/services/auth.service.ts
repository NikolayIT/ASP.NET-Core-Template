import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

import { map } from 'rxjs/operators/map';
import { catchError } from 'rxjs/operators/catchError';
import { Observable } from 'rxjs/Observable';
import { EmptyObservable } from 'rxjs/observable/EmptyObservable';

import { IdentityService } from './identity.service';
import { RouterService } from './router.service';
import { LoggerService } from './logger.service';

import { UserLogin, UserRegister } from '../domain/index';

@Injectable()
export class AuthService {
    private static readonly URLS: any = {
        LOGIN: 'api/account/login',
        REGISTER: 'api/account/register'
    };

    constructor(
        private httpClient: HttpClient,
        private identityService: IdentityService,
        private routerService: RouterService,
        private loggerService: LoggerService)
    { }

    public register(userRegister: UserRegister) {
        return this.httpClient.post(AuthService.URLS.REGISTER, userRegister).pipe(
            map(() => {
                const userLogin = new UserLogin();
                userLogin.email = userRegister.email;
                userLogin.password = userRegister.password;

                return this.login(userLogin).subscribe(
                    () => { },
                    error => this.loggerService.error(error));
            }),
            catchError(err => Observable.throw(err)));
    }

    public login(userLogin: UserLogin): Observable<any> {
        const payload = new HttpParams()
            .set('email', userLogin.email)
            .set('password', userLogin.password)
            .toString();

        const headers = new HttpHeaders().set('Content-Type', 'application/x-www-form-urlencoded');

        return this.httpClient.post(AuthService.URLS.LOGIN, payload, { headers: headers }).pipe(
            map((data: any) => {
                this.identityService.setToken(data['access_token'], data['expires_in']);
                this.identityService.setRoles(data['roles']);
                this.identityService.setEmail(userLogin.email);

                this.routerService.navigateByUrl('/manager');

                return new EmptyObservable();
            }),
            catchError(err => Observable.throw(err)));
    }

    public logout() {
        this.identityService.removeIdentity();
    }
}
