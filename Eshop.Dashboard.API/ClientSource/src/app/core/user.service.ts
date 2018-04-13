import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { environment } from '../../environments/environment';
import { ICredentials } from '../model/ICredentials';
import { IUser } from '../model/IUser';

@Injectable()
export class UserService {
  user: IUser;
  token: string;
  tokenExpiration: string;
  isLoggedIn: boolean = true;

  constructor(private http: HttpClient, private router: Router) {
  }

  getHttpHeaders(): HttpHeaders {
    const header = {
      'Content-Type': 'application/json; charset=utf-8;',
      'Accept': 'application/json'
    };

    // TODO: implement localizatuion header

    if (this.token != null) {
      header['Authorization'] = 'bearer' + this.token;
    }
    return new HttpHeaders(header);
  }

  logout() {
    this.user = null;
    this.token = null;
    this.tokenExpiration = null;
  }
}
