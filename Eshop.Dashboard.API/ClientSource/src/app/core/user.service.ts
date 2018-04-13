import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

import { environment } from '../../environments/environment';
import { ICredentials } from '../model/ICredentials';
import { IUser } from '../model/IUser';
import { LoggerService } from './logger.service';

@Injectable()
export class UserService {
  user: IUser;
  token: string;
  tokenExpiration: string;
  isLoggedIn: boolean = true;

  constructor(private http: HttpClient, private router: Router, private loggerService: LoggerService) {
  }

  getHttpHeaders(): HttpHeaders {
    const header = {
      'Content-Type': 'application/json; charset=utf-8;',
      'Accept': 'application/json'
    };

    // TODO: implement localization header

    if (this.token != null) {
      header['Authorization'] = 'bearer' + this.token;
    }
    return new HttpHeaders(header);
  }

  login(credentials: ICredentials): Observable<any> {
    return this.http.post(environment.apiUrl + 'auth', credentials, { headers: this.getHttpHeaders() });
  }

  register(user: IUser): Observable<IUser> {
    return this.http.post<IUser>(environment.apiUrl + 'users', user, { headers: this.getHttpHeaders() });
  }

  logout() {
    this.user = null;
    this.token = null;
    this.tokenExpiration = null;
  }
}
