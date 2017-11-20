import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from './components/index';

export const APP_ROUTES: Routes = [
    { path: '', component: HomeComponent, pathMatch: 'full' },

    { path: 'account', loadChildren: 'app/components/account/account.module#AccountModule' },
    { path: 'user', loadChildren: 'app/components/user/user.module#UserModule' }
];

@NgModule({
    imports: [RouterModule.forRoot(APP_ROUTES)],
    exports: [RouterModule]
})

export class AppRoutingModule { }
