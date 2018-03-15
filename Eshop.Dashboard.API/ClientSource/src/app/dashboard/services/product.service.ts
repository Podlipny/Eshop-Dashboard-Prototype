import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { UserService } from '../../core/user.service';
import { IProduct } from '../../model/IProduct';
import { environment } from '../../../environments/environment';
import { HttpHelper } from '../../helpers/HttpHelper';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class ProductService {

  constructor(private _userservice: UserService, private http: HttpClient) { }

  loalAllProducts(): Observable<IProduct[]> {
    return this.http.get<IProduct[]>(environment.apiUrl + 'products', { headers: HttpHelper.getHeadres() });
  }
}
