import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { AccountRoutingModule } from './account.routes';

import { ACCOUNT_COMPONENTS } from './index'

@NgModule({
    imports: [
        CommonModule,
        FormsModule,

        AccountRoutingModule
    ],

    declarations: [ACCOUNT_COMPONENTS]
})

export class AccountModule { }
