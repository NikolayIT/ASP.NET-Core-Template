import { Component } from '@angular/core';

import { AuthService } from '../../services/index';

import { UserRegister } from '../../domain/index';

@Component({
    moduleId: module.id,
    selector: 'register',
    templateUrl: 'register.component.html'
})

export class RegisterComponent {
    constructor(private authService: AuthService) { }

    public userRegister: UserRegister = new UserRegister();
    public registerErrorMessage: string = null;

    public register(): void {
        this.authService.register(this.userRegister).subscribe(
            () => { },
            error => { debugger; this.registerErrorMessage = error; });
    }
}