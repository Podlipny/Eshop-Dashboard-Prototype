import { Injectable } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { LoggerService } from './logger.service';
import { UserService } from './user.service';
import { ErrorObservable } from 'rxjs/observable/ErrorObservable';
import 'rxjs/add/observable/throw';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class ErrorHandleService {

  constructor(private loggerService: LoggerService, private userService: UserService) { }

  handle(error: HttpErrorResponse): ErrorObservable {
    switch (error.status) { 
      case 401: {
        this.userService.logout();
        break; 
      } 
      default: { 
        this.loggerService.logError('Status code: ' + error.status + ': ' + error.message); 
        break; 
      }
    }
    return new ErrorObservable(error);
  }
}
