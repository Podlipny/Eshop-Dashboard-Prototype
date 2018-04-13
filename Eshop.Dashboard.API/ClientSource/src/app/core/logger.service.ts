import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

import { environment } from '../../environments/environment';
import { HttpHelper } from '../helpers/HttpHelper';
import { UserService } from './user.service';
import { ILogMessage } from '../model/ILogMessage';
import { ILogEntity } from '../model/ILogEntity';
import { LogLevelEnum } from '../enums/LogLevelEnum';
import { catchError } from 'rxjs/operators/catchError';
import { ErrorObservable } from 'rxjs/observable/ErrorObservable';


@Injectable()
export class LoggerService {
  productsEndpoint: string = 'logs';

  constructor(private http: HttpClient, private userService: UserService) { }

  /**
   * log entity throw API
   * 
   * @param {LogLevelEnum} logLevelId Log level
   * @param {string} message Message to log
   * @memberof LoggerService
   */
  log(logLevelId: LogLevelEnum, message: string) {
    const _message: ILogMessage = {
      logLevelId: logLevelId,
      message: message
    };
    this.http.post<ILogMessage>(environment.apiUrl + this.productsEndpoint + '/', _message, { headers: this.userService.getHttpHeaders() })
             .pipe(catchError((error: HttpErrorResponse) => {
                console.error(error.message);
                return new ErrorObservable(error);
              }));
  }

  logInfo(message: string) {
    this.log(LogLevelEnum.Info, message);
  }

  logTrace(message: string) {
    this.log(LogLevelEnum.Trace, message);
  }

  logWarning(message: string) {
    this.log(LogLevelEnum.Warning, message);
  }

  logEvent(message: string) {
    this.log(LogLevelEnum.Event, message);
  }

  logError(message: string) {
    this.log(LogLevelEnum.Error, message);
  }

  /**
   * Returns list of loged events - only for logged users, which have greater role than customer
   * Role verification is managed by API
   * 
   * @param {number} [pageNumber=2] 
   * @param {number} [pageSize=10] 
   * @param {string} [searchQuery=null] 
   * @returns {Observable<HttpResponse<ILogEntity[]>>} 
   * @memberof LoggerService
   */
  loadLogs(pageNumber: number = 2, pageSize: number = 10, searchQuery: string = null): Observable<HttpResponse<ILogEntity[]>> {
    let query = this.productsEndpoint + '?pageNumber=' + pageNumber + '&pageSize=' + pageSize;

    if (searchQuery) {
      query += '&searchQuery=' + searchQuery;
    }
    return this.http.get<ILogEntity[]>(environment.apiUrl + query, { headers: HttpHelper.getHeadres(), observe: 'response' })
                    .pipe(catchError((error: HttpErrorResponse) => {
                        this.logError(error.message);
                        console.error(error.message);
                        return new ErrorObservable(error);
                      }));
  }
}
