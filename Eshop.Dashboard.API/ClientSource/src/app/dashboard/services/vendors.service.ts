import { Injectable } from '@angular/core';
import { HttpResponse, HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { HttpHelper } from '../../helpers/HttpHelper';
import { UserService } from '../../core/user.service';
import { ToastService } from '../../core/toast/toast.service';
import { ErrorHandleService } from '../../core/error-handle.service';
import { IVendor } from '../../model/IVendor';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/catch';

@Injectable()
export class VendorsService {
  vendorsEndpoint: string = 'vendors';

  constructor(private userService: UserService,
              private toasterService: ToastService,
              private errorHandler: ErrorHandleService,
              private http: HttpClient) { }

  loalAll(orderBy: string = null, pageNumber: number = 2, pageSize: number = 10, searchQuery: string = null, sortOrder: string = 'dest'): Observable<HttpResponse<IVendor[]>> {
    let query = this.vendorsEndpoint + '?';

    if (orderBy) {
      query += 'orderBy=' + orderBy + ' ' + sortOrder;
    }
    query += '&pageNumber=' + pageNumber + '&pageSize=' + pageSize;

    if (searchQuery) {
      query += '&searchQuery=' + searchQuery;
    }
    return this.http.get<IVendor[]>(environment.apiUrl + query, { headers: HttpHelper.getHeaders(this.userService.token), observe: 'response' })
                    .catch(this.errorHandler.handle);
  }
}
