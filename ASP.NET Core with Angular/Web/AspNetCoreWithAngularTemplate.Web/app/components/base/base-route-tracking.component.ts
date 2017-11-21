import { ActivatedRoute } from '@angular/router';

import { BaseSubscriptionsComponent } from './base-subscriptions.component';

export abstract class BaseRouteTrackingComponent extends BaseSubscriptionsComponent {
    constructor(protected activatedRoute: ActivatedRoute) {
        super();
    }

    protected routeParams: any;

    // Plugability point
    protected onRouteChange(): void { }

    protected onInit(): void {
        super.onInit();

        this.subscriptions.push(this.activatedRoute.params.subscribe(params => {
            this.routeParams = params;

            this.onRouteChange();
        }));
    }
}
