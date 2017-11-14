import { Injectable } from '@angular/core';

@Injectable()
export class StorageService {
    public getItem(key: string): string {
        return localStorage.getItem(key);
    }

    public setItem(key: string, data: any): void {
        if (data && typeof data !== 'string') {
            if (typeof data === 'object') {
                data = JSON.stringify(data);
            } else {
                data = data.toString();
            }
        }

        localStorage.setItem(key, data);
    }

    public removeItem(key: string): void {
        localStorage.removeItem(key);
    }
}
