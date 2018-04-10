import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { IUser } from '../model/IUser';
import { environment } from '../../environments/environment';
import { Router } from '@angular/router';
import { ICredentials } from '../model/ICredentials';


@Injectable()
export class UserService {
  user: IUser;
  token: string;
  tokenExpiration: string;
  isLoggedIn: boolean = true;

  constructor(private http: HttpClient, private router: Router) {
  }

  logout() {
    this.user = null;
  }
}
