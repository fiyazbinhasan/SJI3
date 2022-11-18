import { Injectable } from '@angular/core';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { BehaviorSubject } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class AuthService {
  isAuthenticated$: BehaviorSubject<boolean> = new BehaviorSubject(false);
  userData$: BehaviorSubject<any> = new BehaviorSubject({});
  token$: BehaviorSubject<string> = new BehaviorSubject('');

  constructor(private oidcSecurityService: OidcSecurityService) {}

  checkAuth() {
    this.oidcSecurityService.checkAuth().subscribe(({ isAuthenticated }) => {
      this.isAuthenticated$.next(isAuthenticated);
      console.log('app authenticated', isAuthenticated);
    });

    this.oidcSecurityService.userData$.subscribe((userDataResult) => {
      this.userData$.next(userDataResult.userData);
    });
  }

  authorize() {
    this.oidcSecurityService.authorize();
  }

  logoffAndRevokeTokens() {
    this.oidcSecurityService
      .logoffAndRevokeTokens()
      .subscribe((result) => console.log(result));
  }

  revokeRefreshToken() {
    this.oidcSecurityService
      .revokeRefreshToken()
      .subscribe((result) => console.log(result));
  }

  revokeAccessToken() {
    this.oidcSecurityService
      .revokeAccessToken()
      .subscribe((result) => console.log(result));
  }

  logToken() {
    this.oidcSecurityService
      .getAccessToken()
      .subscribe((token) => console.log(token));
  }

  getAccessToken() {
    return this.oidcSecurityService.getAccessToken();
  }
}
