import { Injectable } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { LoggerService } from './logger.service';
import { UserService } from './user.service';

@Injectable()
export class ErrorHandleService {

  constructor(private loggerService: LoggerService, private userService: UserService) { }

  handle(error: HttpErrorResponse) {
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
  }
}
