import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import {
    TodoItemCreateComponent,
    TodoItemsComponent,

    UserComponent
} from './index';

import { AuthGuardService } from '../../services/index';

const ACCOUNT_ROUTES: Routes = [
    {
        path: '',
        component: UserComponent,
        canActivate: [AuthGuardService],
        canActivateChild: [AuthGuardService],
        children: [
            { path: '', redirectTo: '/user/todo-items', pathMatch: 'full' },

            { path: 'todo-item-create', component: TodoItemCreateComponent },
            { path: 'todo-items', component: TodoItemsComponent }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(ACCOUNT_ROUTES)],
    exports: [RouterModule]
})

export class UserRoutingModule { }
