import { OidcSecurityService, UserDataResult } from 'angular-auth-oidc-client';
import { Component, OnInit } from '@angular/core';
import { Observable, of } from 'rxjs';
import { AuthService } from './common/services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
})
export class AppComponent implements OnInit {
  title = '';
  userData: any;
  isAuthenticated = false;

  constructor(private authService: AuthService) {
    console.log('AppComponent STARTING');
  }

  ngOnInit(): void {
    this.authService.checkAuth();

    this.authService.isAuthenticated$.subscribe((isAuthenticated) => {
      this.isAuthenticated = isAuthenticated;
    });

    this.authService.userData$.subscribe((userDataResult) => {
      this.userData = userDataResult;
    });
  }

  login(): void {
    console.log('start login');
    this.authService.authorize();
  }

  refreshSession(): void {
    console.log('start refreshSession');
    this.authService.authorize();
  }

  logoffAndRevokeTokens(): void {
    this.authService.logoffAndRevokeTokens();
  }

  revokeRefreshToken(): void {
    this.authService.revokeRefreshToken();
  }

  revokeAccessToken(): void {
    this.authService.revokeAccessToken();
  }

  logToken() {
    this.authService.logToken();
  }
}
