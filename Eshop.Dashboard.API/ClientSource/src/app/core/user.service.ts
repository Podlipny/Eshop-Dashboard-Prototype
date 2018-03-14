import { Injectable } from '@angular/core';
import { IUser } from '../model/user';

@Injectable()
export class UserService {
  user: IUser;
  token: string;
  tokenExpiration: string;
  isLoggedIn: boolean = true;

  login(userobj: IUser, rememberme: boolean = false) {
    this.user = userobj;

    // TODO: call navigation to dashboard
    // TODO: access API to recieve JWT token
    // TODO: save token to localstorage
  }

  register(userobj: IUser) {
    this.user = userobj;

    // TODO: call navigation to login
  }

  logout() {
    this.user = null;
  }
}
