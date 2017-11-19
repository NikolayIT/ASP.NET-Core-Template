import { Injectable } from '@angular/core';
import { CanActivate, CanActivateChild, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';

import { AuthService } from '../auth.service';
import { RouterService } from '../router.service';

@Injectable()
export class AuthNoGuardService implements CanActivate, CanActivateChild {
    constructor(private routerService: RouterService, private authService: AuthService) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        return this.checkNotAuth();
    }

    canActivateChild(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        return this.checkNotAuth();
    }

    checkNotAuth() {
        if (!this.authService.isAuthorized()) {
            return true;
        }

        this.routerService.redirectToHome();

        return false;
    }
}