import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { UserRoutingModule } from './user.routes';

import { USER_COMPONENTS } from './index'

@NgModule({
    imports: [
        CommonModule,
        FormsModule,

        UserRoutingModule
    ],

    declarations: [USER_COMPONENTS]
})

export class UserModule { }
