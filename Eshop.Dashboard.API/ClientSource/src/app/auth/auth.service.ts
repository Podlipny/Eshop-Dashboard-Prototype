import { Injectable } from '@angular/core';
import { ICredentials } from '../model/ICredentials';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { LoggerService } from '../core/logger.service';
import { Observable } from 'rxjs/Observable';
import { environment } from '../../environments/environment';
import { IUser } from '../model/IUser';
import { HttpHelper } from '../helpers/HttpHelper';
import { catchError } from 'rxjs/operators/catchError';
import { UserService } from '../core/user.service';

@Injectable()
export class AuthService {

  constructor(private http: HttpClient) { }

  login(credentials: ICredentials): Observable<any> {
    return this.http.post<ICredentials>(environment.apiUrl + 'auth', credentials, { headers: HttpHelper.getHeaders() });
  }

  register(user: IUser): Observable<any> {
    return this.http.post<IUser>(environment.apiUrl + 'users', user, { headers: HttpHelper.getHeaders() });
  }
}
