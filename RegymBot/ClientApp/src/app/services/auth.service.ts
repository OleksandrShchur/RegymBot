import { Injectable } from '@angular/core';
import { HttpClient, HttpRequest } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { CredentialsModel } from '../models/credentials-model';
import { LocalStorageKey } from '../constants/local-storage-keys';

@Injectable()
export class AuthService {
    cachedRequests: Array<HttpRequest<any>> = [];
    private url: string;
    private authorizedUser: CredentialsModel | null = null;
    jwtHelper: JwtHelperService;
    get authorized(): boolean {
        return this.authorizedUser != null && !!this.authorizedUser.token && !this.tokenExpired;
    }
    get tokenExpired(): boolean {
        return this.currentUser == null || this.jwtHelper.isTokenExpired(this.currentUser.token);
    }
    get currentUser(): CredentialsModel | null {
        return this.authorizedUser;
    }

    constructor(
        private router: Router,
        private route: ActivatedRoute,
        private http: HttpClient,
    ) {
        this.jwtHelper = new JwtHelperService();
        this.url = '/api/auth';

        const jsonUser = localStorage.getItem(LocalStorageKey.CurrentUser);
        if (jsonUser) {
            this.authorizedUser = JSON.parse(jsonUser);
        }
    }

    public async ping(): Promise<boolean> {
        return true; // TODO: UNMOCK
        try {
            await this.http.get(this.url + '/ping').toPromise();
        } catch (error) {
            if (error.status && error.status === 401) {
                return false;
            }
        }
        return true;
    }

    public async auth(login: string, password: string): Promise<CredentialsModel> {
        this.authorizedUser = await this.http.post<CredentialsModel>(this.url + '/login', { login, password }).toPromise();

        if (!this.authorized) {
            throw new Error('User is not authorized');
        }

        localStorage.setItem(LocalStorageKey.CurrentUser, JSON.stringify(this.authorizedUser));
        return this.authorizedUser;
    }

    public logout() {
        this.clearStoredData();
        this.authorizedUser = null;
        this.router.navigate(['login']);
    }

    clearStoredData() {
        localStorage.removeItem(LocalStorageKey.CurrentUser);
    }

    // public isAdmin() {
    //   return ROLES.ADMIN.includes(this.currentUser.role);
    // }

    public collectFailedRequest(request: HttpRequest<any>): void {
        this.cachedRequests.push(request);
    }
    public retryFailedRequests(): void {
        // retry the requests. this method can
        // be called after the token is refreshed
      }

}

