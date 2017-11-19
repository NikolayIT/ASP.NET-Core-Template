import { Component } from '@angular/core';

import { AuthService } from '../../services/index';

import { UserLogin } from '../../domain/index';

@Component({
    moduleId: module.id,
    selector: 'login',
    templateUrl: 'login.component.html'
})

export class LoginComponent {
    constructor(private authService: AuthService) { }

    public userLogin: UserLogin = new UserLogin();
    public loginErrorMessage: string = null;

    public login(): void {
        this.authService.login(this.userLogin).subscribe(
            () => { },
            error => { debugger; this.loginErrorMessage = error; });
    }
}