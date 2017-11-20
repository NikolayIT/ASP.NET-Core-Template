import { Injectable } from '@angular/core';
import { Router, NavigationStart, NavigationEnd, NavigationCancel, NavigationError } from '@angular/router';

import { Subscription } from 'rxjs/Subscription';
import { ReplaySubject } from 'rxjs/ReplaySubject';

import { WindowRefService } from './window-ref.service';

@Injectable()
export class RouterService {
    constructor(private router: Router, private windowRefService: WindowRefService) {
        this.router.events.subscribe((e: any) => {
            if (e instanceof NavigationStart) {
                this.routes[e.url] = true;
                this.onStart.next(true);
            } else if (e instanceof NavigationEnd || e instanceof NavigationCancel || e instanceof NavigationError) {
                delete this.routes[e.url];
                if (Object.getOwnPropertyNames(this.routes).length === 0) {
                    this.onEnd.next(true);
                }
            }
        });
    }

    private routes: any = {};

    private onStart: ReplaySubject<any> = new ReplaySubject(1);
    private onEnd: ReplaySubject<any> = new ReplaySubject(1);

    public getUrl(): string {
        return this.router.url;
    }

    public navigateByUrl(url: string): void {
        this.router.navigateByUrl(url);
    }

    public redirectToLogin(): void {
        this.navigateByUrl('/account/login');
    }

    public redirectToHome(): void {
        this.navigateByUrl('/');
    }

    public reloadApp(): void {
        this.windowRefService.nativeWindow.location.reload(true);
    }

    public subscribeOnStart(observerOrNext): Subscription {
        return this.onStart.subscribe(observerOrNext);
    }

    public subscribeOnEnd(observerOrNext): Subscription {
        return this.onEnd.subscribe(observerOrNext);
    }
}
