import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { LocationStrategy, HashLocationStrategy } from '@angular/common';

import { AppRoutingModule } from './app.routes';

import { APP_SERVICES, AuthInterceptorService, AuthErrorsInterceptorService } from './services/index';

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
        { provide: LocationStrategy, useClass: HashLocationStrategy },
        { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptorService, multi: true },
        { provide: HTTP_INTERCEPTORS, useClass: AuthErrorsInterceptorService, multi: true }
    ],

    bootstrap: [AppComponent]
})

export class AppModule { }
