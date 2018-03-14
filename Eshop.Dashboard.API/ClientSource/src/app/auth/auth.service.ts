import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { IUser } from '../model/user';
import { environment } from '../../environments/environment';
import { ICredentials } from '../model/ICredentials';
import { UserService } from '../core/user.service';


@Injectable()
export class AuthService {

  constructor(private _userservice: UserService, private http: HttpClient) {
  }

  login(credentialsObj: ICredentials): Observable<any> {
    return this.http.post(environment.apiUrl + 'auth', credentialsObj);

    // TODO: call navigation to dashboard
    // TODO: access API to recieve JWT token
    // TODO: save token to localstorage
  }

  register(userobj: IUser): Observable<IUser> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8;',
      'Accept': '*/*'
    });

    return this.http.post<IUser>(environment.apiUrl + 'users', userobj, { headers: headers });
  }

  logout() {
    this._userservice.user = null;
    this._userservice.token = null;
    this._userservice.tokenExpiration = null;
  }
}
