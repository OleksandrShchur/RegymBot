import { Injectable, Inject } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({
    providedIn: 'root'
})
export class NotificationsService {

    defaultErrorConfig: NotificationProperties = { buttonText: 'OK', class: 'doct-notification-error', duration: 5000 };
    defaultInfoConfig: NotificationProperties = { buttonText: 'OK', class: 'doct-notification-info', duration: 5000 };
    defaultWarnConfig: NotificationProperties = { buttonText: 'OK', class: 'doct-notification-warn', duration: 5000 };

    constructor(
        private snackbar: MatSnackBar
    ) {
    }

    public error(message: string, config?: NotificationProperties): void {
        config = {...this.defaultErrorConfig, ...config};
        this.snackbar.open(message, 'OK', {
            duration: config.duration,
            panelClass: config.class,
        });
    }
    public info(message: string, config?: NotificationProperties): void {
        config = {...this.defaultInfoConfig, ...config};
        this.snackbar.open(message, 'OK', {
            duration: config.duration,
            panelClass: config.class,
        });
    }
    public warn(message: string, config?: NotificationProperties): void {
        config = {...this.defaultWarnConfig, ...config};
        this.snackbar.open(message, 'OK', {
            duration: config.duration,
            panelClass: config.class,
        });
    }
}

export interface NotificationProperties {
    buttonText?: string;
    class?: string;
    duration?: number;
}
