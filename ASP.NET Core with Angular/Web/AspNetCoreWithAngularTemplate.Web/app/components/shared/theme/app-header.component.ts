import { Component } from '@angular/core';

import { AuthService } from '../../../services/index';

import { BaseSubscriptionsComponent } from '../../base/base-subscriptions.component';

@Component({
    moduleId: module.id,
    selector: 'app-header',
    templateUrl: 'app-header.component.html'
})

export class AppHeaderComponent extends BaseSubscriptionsComponent {
    constructor(private authService: AuthService) {
        super();
    }

    public isUserAuthorized: boolean = false;

    public logout(): void {
        this.authService.logout();
    }

    protected onInit(): void {
        this.subscriptions.push(this.authService.isAuthorized$.subscribe(
            (isAuthorized: boolean) => this.isUserAuthorized = isAuthorized));
    }
}
