import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

import { AppFooterComponent } from './theme/app-footer.component';
import { AppHeaderComponent } from './theme/app-header.component';

import { APP_DIRECTIVES } from '../../directives/index';
import { APP_PIPES } from '../../pipes/index';

@NgModule({
    imports: [CommonModule, FormsModule, RouterModule],
    declarations: [
        AppFooterComponent,
        AppHeaderComponent,

        APP_DIRECTIVES,
        APP_PIPES
    ],
    exports: [
        CommonModule,
        FormsModule,
        RouterModule,

        AppFooterComponent,
        AppHeaderComponent,

        APP_DIRECTIVES,
        APP_PIPES
    ]
})

export class SharedModule { }
