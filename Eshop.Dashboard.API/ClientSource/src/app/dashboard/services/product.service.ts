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
  productsEndpoint: string = 'dashboardProducts';

  constructor(private _userservice: UserService, private http: HttpClient) { }

  getProduct(id: string): Observable<IProduct> {
    return this.http.get<IProduct>(environment.apiUrl + this.productsEndpoint + '/' + id, { headers: HttpHelper.getHeadres() });
  }

  loalProducts(orderBy: string = null, pageNumber: number = 2, pageSize: number = 10, searchQuery: string = null, sortOrder: string = 'dest'): Observable<HttpResponse<IProduct[]>> {
    let query = this.productsEndpoint + '?';

    if (orderBy) {
      query += 'orderBy=' + orderBy + ' ' + sortOrder;
    }
    query += '&pageNumber=' + pageNumber + '&pageSize=' + pageSize;

    if (searchQuery) {
      query += '&searchQuery=' + searchQuery;
    }
    return this.http.get<IProduct[]>(environment.apiUrl + query, { headers: HttpHelper.getHeadres(), observe: 'response' });
  }
}
