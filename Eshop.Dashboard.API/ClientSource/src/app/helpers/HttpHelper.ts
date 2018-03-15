import { HttpHeaders } from '@angular/common/http';

export class HttpHelper {

  static getHeadres(authToken: string = null): HttpHeaders {
    const header = {
      'Content-Type': 'application/json; charset=utf-8;',
      'Accept': 'application/json'
    };

    if (authToken != null) {
      header['Authorization'] = 'bearer' + authToken;
    }
    return new HttpHeaders(header);
  }
}
