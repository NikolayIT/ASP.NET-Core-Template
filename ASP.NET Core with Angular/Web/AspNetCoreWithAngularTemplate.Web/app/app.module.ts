import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { LocationStrategy, HashLocationStrategy } from '@angular/common';

import { AppRoutingModule } from './app.routes';

import { APP_SERVICES } from './services/index';

import { APP_COMPONENTS, AppComponent } from './components/index';

import { SharedModule } from './components/shared/shared.module';

@NgModule({
    imports: [
        BrowserModule,
        FormsModule,
        HttpClientModule,

        SharedModule,

        AppRoutingModule
    ],

    declarations: [APP_COMPONENTS],

    providers: [
        APP_SERVICES,
        {
            provide: LocationStrategy,
            useClass: HashLocationStrategy
        }
    ],

    bootstrap: [AppComponent]
})

export class AppModule { }
