import { Component } from '@angular/core';

import { AuthService, RouterService } from '../../../services/index';

@Component({
    moduleId: module.id,
    selector: 'app-header',
    templateUrl: 'app-header.component.html'
})

export class AppHeaderComponent {
    constructor(private authService: AuthService, private routerService: RouterService) { }

    public isUserAuthorized: boolean = this.authService.isAuthorized();

    public logout(): void {
        this.authService.logout();
    }
}
