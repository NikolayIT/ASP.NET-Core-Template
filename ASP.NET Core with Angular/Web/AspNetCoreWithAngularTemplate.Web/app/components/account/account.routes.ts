import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import {
    AccountComponent,
    LoginComponent,
    RegisterComponent
} from './index';

import { AuthNoGuardService } from '../../services/index';

const ACCOUNT_ROUTES: Routes = [
    {
        path: '',
        component: AccountComponent,
        canActivate: [AuthNoGuardService],
        canActivateChild: [AuthNoGuardService],
        children: [
            { path: '', redirectTo: '/account/login', pathMatch: 'full' },

            { path: 'login', component: LoginComponent },
            { path: 'register', component: RegisterComponent }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(ACCOUNT_ROUTES)],
    exports: [RouterModule]
})

export class AccountRoutingModule { }
