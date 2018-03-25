import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';

import { UserService } from '../../core/user.service';
import { IProduct } from '../../model/IProduct';
import { environment } from '../../../environments/environment';
import { HttpHelper } from '../../helpers/HttpHelper';
import { Observable } from 'rxjs/Observable';
import { SortOrderEnum } from '../../Enums/SortOrderEnum';

@Injectable()
export class ProductService {

  constructor(private _userservice: UserService, private http: HttpClient) { }

  loalProducts(orderBy: string = null, pageNumber: number = 2, pageSize: number = 10, searchQuery: string = null, sortOrder: string = 'dest'): Observable<HttpResponse<IProduct[]>> {
    let productsEndpoint = 'products?';

    if (orderBy) {
      productsEndpoint += 'orderBy=' + orderBy + ' ' + sortOrder;
    }
    productsEndpoint += '&pageNumber=' + pageNumber + '&pageSize=' + pageSize;

    if (searchQuery) {
      productsEndpoint += '&searchQuery=' + searchQuery;
    }
    return this.http.get<IProduct[]>(environment.apiUrl + productsEndpoint, { headers: HttpHelper.getHeadres(), observe: 'response' });
  }
}
