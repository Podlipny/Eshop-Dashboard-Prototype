import { Injectable, Optional, SkipSelf } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

import { environment } from '../../environments/environment';
import { ICredentials } from '../model/ICredentials';
import { IUser } from '../model/IUser';
import { LoggerService } from './logger.service';

export const TOKEN_KEY = 'TOKEN_KEY';

@Injectable()
export class UserService {
  user: IUser;
  token: string;
  tokenExpiration: number;

  constructor(@Optional() @SkipSelf() userService: UserService, private http: HttpClient, private router: Router) {
    if (this.token == null || this.token === undefined) {
      const tokenKey = JSON.parse(localStorage.getItem(TOKEN_KEY));
      if (tokenKey != null) {
        // token is expired - TODO: verify with API
        if (tokenKey.expiration < Date.now()) {
          this.logout();
        } else {
          // this.authorize(tokenKey.token, Date.parse(tokenKey.expiration));
          this.token = tokenKey.token;
          this.tokenExpiration = tokenKey.expiration;
        }
      }
    }

    if (userService) { 
      return userService; 
    }
    console.log('UserService Created');
  }

  loadUser() {
    // TODO: load user
  }

  authorize(token: string, expiration: number) {
    this.token = token;
    this.tokenExpiration = expiration;
    localStorage.setItem(TOKEN_KEY, JSON.stringify({ token: token, expiration: expiration }));
  }

  isAuthorized(): boolean {
    return this.token != null && this.tokenExpiration >= Date.now();
  }

  logout() {
    localStorage.removeItem(TOKEN_KEY);
    this.user = null;
    this.token = null;
    this.tokenExpiration = null;
  }
}
