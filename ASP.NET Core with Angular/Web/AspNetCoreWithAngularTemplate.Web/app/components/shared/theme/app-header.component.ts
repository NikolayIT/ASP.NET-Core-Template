import { Component } from '@angular/core';

import { AuthService, IdentityService } from '../../../services/index';

import { BaseSubscriptionsComponent } from '../../base/base-subscriptions.component';

@Component({
    moduleId: module.id,
    selector: 'app-header',
    templateUrl: 'app-header.component.html'
})

export class AppHeaderComponent extends BaseSubscriptionsComponent {
    constructor(private authService: AuthService, private identityService: IdentityService) {
        super();
    }

    public isUserAuthorized: boolean = false;
    public userEmail: string = null;

    public logout(): void {
        this.authService.logout();
    }

    protected onInit(): void {
        this.subscriptions.push(this.authService.isAuthorized$.subscribe(
            (isAuthorized: boolean) => {
                this.isUserAuthorized = isAuthorized;
                this.userEmail = isAuthorized ? this.identityService.getEmail() : null;
            }));
    }
}
