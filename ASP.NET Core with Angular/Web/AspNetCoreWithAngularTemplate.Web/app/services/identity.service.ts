import { Injectable } from '@angular/core';

import { StorageService } from './storage.service';

@Injectable()
export class IdentityService {
    private static readonly EMAIL_KEY = 'IDENTITY_EMAIL';
    private static readonly TOKEN_KEY = 'IDENTITY_TOKEN';
    private static readonly TOKEN_EXPIRY_KEY = 'IDENTITY_TOKEN_EXPIRY';
    private static readonly ROLES_KEY = 'IDENTITY_ROLES';

    constructor(private storageService: StorageService) { }

    private token: string = null;
    private tokenExpiry: number = null;
    private email: string = null;
    private roles: any = null;

    public getEmail(): string {
        if (!this.email) {
            this.email = this.storageService.getItem(IdentityService.EMAIL_KEY);
        }

        return this.email;
    }

    public getToken(): string {
        if (!this.token) {
            const persistedToken = this.storageService.getItem(IdentityService.TOKEN_KEY);
            if (persistedToken) {
                this.token = persistedToken;
                this.tokenExpiry =
                    new Date().getTime() + parseInt(this.storageService.getItem(IdentityService.TOKEN_EXPIRY_KEY));
            }
        }

        if (this.tokenExpiry && new Date().getTime() > this.tokenExpiry) {
            this.removeToken();
        }

        return this.token;
    }

    public getRoles(): string {
        if (!this.roles) {
            const persistedRoles = this.storageService.getItem(IdentityService.ROLES_KEY);
            if (persistedRoles) {
                this.roles = JSON.parse(persistedRoles);
            }
        }

        return this.roles;
    }

    public setEmail(email: string): void {
        this.email = email;
        this.storageService.setItem(IdentityService.EMAIL_KEY, email);
    }

    public setToken(token: string, tokenExpiry: number): void {
        this.token = token;
        this.tokenExpiry = new Date().getTime() + tokenExpiry;

        this.storageService.setItem(IdentityService.TOKEN_KEY, token);
        this.storageService.setItem(IdentityService.TOKEN_EXPIRY_KEY, tokenExpiry);
    }

    public setRoles(roles: any): void {
        this.roles = roles;
        this.storageService.setItem(IdentityService.ROLES_KEY, roles);
    }

    public removeIdentity(): void {
        this.removeEmail();
        this.removeToken();
        this.removeRoles();
    }

    private removeEmail(): void {
        this.email = null;
        this.storageService.removeItem(IdentityService.EMAIL_KEY);
    }

    private removeToken(): void {
        this.token = null;
        this.tokenExpiry = null;
        this.storageService.removeItem(IdentityService.TOKEN_KEY);
        this.storageService.removeItem(IdentityService.TOKEN_EXPIRY_KEY);
    }

    private removeRoles(): void {
        this.roles = null;
        this.storageService.removeItem(IdentityService.ROLES_KEY);
    }
}
