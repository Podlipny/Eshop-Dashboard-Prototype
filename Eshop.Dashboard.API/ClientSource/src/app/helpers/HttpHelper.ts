import { HttpHeaders } from '@angular/common/http';

export class HttpHelper {

  static getHeaders(authToken: string = null): HttpHeaders {
    const header = {
      'Content-Type': 'application/json; charset=utf-8;',
      'Accept': 'application/json'
    };

    // TODO: implement localization header
    
    if (authToken != null) {
      header['Authorization'] = 'bearer' + authToken;
    }
    return new HttpHeaders(header);
  }
}
