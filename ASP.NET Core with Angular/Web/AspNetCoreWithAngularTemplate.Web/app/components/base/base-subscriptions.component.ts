import { OnInit, OnDestroy } from '@angular/core';

export abstract class BaseSubscriptionsComponent implements OnInit, OnDestroy {
    protected subscriptions: any[] = [];

    // Plugability point
    protected onInit(): void { }

    // Plugability point
    protected onDestroy(): void { }

    ngOnInit(): void {
        this.onInit();
    }

    ngOnDestroy(): void {
        this.subscriptions.forEach(subscription => subscription.unsubscribe());

        this.onDestroy();
    }
}
