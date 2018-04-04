import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { IUser } from '../model/IUser';
import { environment } from '../../environments/environment';
import { ICredentials } from '../model/ICredentials';
import { UserService } from '../core/user.service';
import { HttpHelper } from '../helpers/HttpHelper';


@Injectable()
export class AuthService {

  constructor(private _userservice: UserService, private http: HttpClient) {
  }

  login(credentialsObj: ICredentials): Observable<any> {
    return this.http.post(environment.apiUrl + 'auth', credentialsObj);
  }

  register(userobj: IUser): Observable<IUser> {
    return this.http.post<IUser>(environment.apiUrl + 'users', userobj, { headers: HttpHelper.getHeadres() });
  }

  logout() {
    this._userservice.user = null;
    this._userservice.token = null;
    this._userservice.tokenExpiration = null;
  }
}
