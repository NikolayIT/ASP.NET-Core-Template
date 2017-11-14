import { Injectable } from '@angular/core';

@Injectable()
export class LoggerService {
    public log(message: string): void {
        console.log(this.format(message));
    }

    public logWithColor(message: any, color: string = '#3F73F8'): void {
        console.log(`%c${this.format(message)}`, `color:${color};`);
    }

    public warn(message: string): void {
        console.warn(this.format(message));
    }

    public error(message: string): void {
        console.error(this.format(message));
    }

    private format(message: string) {
        const date = new Date();
        return `[${date.toISOString()}] ${message}`;
    }
}
