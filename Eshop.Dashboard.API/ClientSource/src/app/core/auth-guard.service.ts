import { Injectable } from '@angular/core';
import {
  CanActivate,
  CanActivateChild,
  CanLoad,
  Route,
  Router,
  ActivatedRouteSnapshot,
  RouterStateSnapshot
} from '@angular/router';

import { UserService } from './user.service';
import { LoginComponent } from '../auth/login/login.component';

@Injectable()
export class AuthGuard implements CanActivate, CanActivateChild, CanLoad {
  constructor(private userService: UserService, private router: Router) { }

  canLoad(route: Route) {
    if (this.userService.isAuthorized()) {
      return true;
    }

    const url = `/${route.path}`;
    this.router.navigate(['/login']);

    return this.userService.isAuthorized();
  }

  canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    if (this.userService.isAuthorized()) {
      // if user is authorized, we wont let him go to login page
      if (next.component === LoginComponent) {
        this.router.navigate(['/']);
        return false;
      }
      return true;
    }
    if (next.component === LoginComponent) {
      return true;
    }

    this.router.navigate(['/login']);

    return false;
  }

  canActivateChild(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    return this.canActivate(route, state);
  }
}
