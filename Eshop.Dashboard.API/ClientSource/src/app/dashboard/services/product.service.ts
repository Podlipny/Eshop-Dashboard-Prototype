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
  products: IProduct[] = [];
  totalCount: number = this.products.length;
  loading: boolean = false;

  constructor(private _userservice: UserService, private http: HttpClient) { }

  loalProducts(orderBy: string = null, pageNumber: number = 2, pageSize: number = 10, searchQuery: string = null, sortOrder: string = 'dest') {
    this.loading = true;
    let productsEndpoint = 'products?';

    if (orderBy) {
      productsEndpoint += 'orderBy=' + orderBy + ' ' + sortOrder;
    }
    productsEndpoint += '&pageNumber=' + pageNumber + '&pageSize=' + pageSize;

    if (searchQuery) {
      productsEndpoint += '&searchQuery=' + searchQuery;
    }
    this.http.get<IProduct[]>(environment.apiUrl + productsEndpoint, { headers: HttpHelper.getHeadres(), observe: 'response' })
      .subscribe((res: HttpResponse<IProduct[]>) => {
        this.products = res.body;

        // we have to set x-pagination to COSR rules on API server
        const xPagination = res.headers.get('x-pagination');
        this.totalCount = JSON.parse(xPagination).totalCount;

        this.loading = false;
        // Hack - because we are setting loading and until data are loaded
        // we have to stop changeDetection and tell angular when to detect changes
        // - this happens only when we are changing pagesize
      }, error => {
        console.log(error.error);
      });
  }
}
