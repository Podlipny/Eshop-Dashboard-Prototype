import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse, HttpErrorResponse } from '@angular/common/http';

import { UserService } from '../../core/user.service';
import { IProduct } from '../../model/IProduct';
import { environment } from '../../../environments/environment';
import { HttpHelper } from '../../helpers/HttpHelper';

import { SortOrderEnum } from '../../Enums/SortOrderEnum';

import { Observable } from 'rxjs/Observable';
import { catchError } from 'rxjs/operators';

import { ToastService } from '../../core/toast/toast.service';
import { ErrorObservable } from 'rxjs/observable/ErrorObservable';

@Injectable()
export class ProductService {
  productsEndpoint: string = 'dashboard/DashboardProducts';

  constructor(private userService: UserService,
              private toasterService: ToastService,
              private http: HttpClient) { }

  addProduct(product: IProduct): Observable<IProduct> {
    return this.http.post<IProduct>(environment.apiUrl + this.productsEndpoint + '/', product, { headers: HttpHelper.getHeadres() });
  }

  updateProduct(product: IProduct): Observable<IProduct> {
    return this.http.put<IProduct>(environment.apiUrl + this.productsEndpoint + '/', product, { headers: HttpHelper.getHeadres() });
  }

  deleteProduct(id: string): Observable<{}> {
    return this.http.delete(environment.apiUrl + this.productsEndpoint + '/' + id, { headers: HttpHelper.getHeadres() });
  }

  getProduct(id: string): Observable<IProduct> {
    return this.http.get<IProduct>(environment.apiUrl + this.productsEndpoint + '/' + id, { headers: HttpHelper.getHeadres() });
                    // .pipe(catchError((error: HttpErrorResponse) => {
                    //     console.error('', error);
                    //     return new ErrorObservable('Something bad happened; please try again later.');
                    //   })
                    // );
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
    return this.http.get<IProduct[]>(environment.apiUrl + query, { headers: HttpHelper.getHeadres(), observe: 'response' })
                    .pipe(catchError((error: HttpErrorResponse) => {
                        console.error(error.message);
                        return new ErrorObservable(error);
                      }));
  }
}
