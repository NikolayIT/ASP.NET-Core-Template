import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

import { map } from 'rxjs/operators/map';
import { catchError } from 'rxjs/operators/catchError';
import { Observable } from 'rxjs/Observable';
import { ErrorObservable } from 'rxjs/observable/ErrorObservable';
import { EmptyObservable } from 'rxjs/observable/EmptyObservable';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';

import { IdentityService } from './identity.service';
import { RouterService } from './router.service';
import { LoggerService } from './logger.service';

import { UserLogin, UserRegister } from '../domain/index';

@Injectable()
export class AuthService {
    public static readonly URLS: any = {
        LOGIN: 'api/account/login',
        REGISTER: 'api/account/register'
    };

    constructor(
        private httpClient: HttpClient,
        private identityService: IdentityService,
        private routerService: RouterService,
        private loggerService: LoggerService)
    { }

    private isAuthorizedSubject = new BehaviorSubject<boolean>(this.isAuthorized());

    public isAuthorized$ = this.isAuthorizedSubject.asObservable();

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
            catchError(err => ErrorObservable.create(err)));
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

                this.isAuthorizedSubject.next(true);

                this.routerService.redirectToHome();

                return EmptyObservable.create();
            }),
            catchError(err => ErrorObservable.create(err)));
    }

    public logout() {
        this.identityService.removeIdentity();

        this.isAuthorizedSubject.next(false);

        this.routerService.redirectToHome();
    }

    public isAuthorized(): boolean {
        return this.identityService.getToken() !== null;
    }
}
