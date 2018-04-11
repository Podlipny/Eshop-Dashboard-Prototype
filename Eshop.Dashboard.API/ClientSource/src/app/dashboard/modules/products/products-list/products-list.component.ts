import { Component, OnInit, ChangeDetectionStrategy, AfterViewInit, ChangeDetectorRef } from '@angular/core';
import { HttpResponse } from '@angular/common/http';
import { TdDataTableService, TdDataTableSortingOrder, ITdDataTableSortChangeEvent } from '@covalent/core/data-table';
import { IPageChangeEvent } from '@covalent/core/paging';
import { ProductService } from '../../../services/product.service';
import { IDatatableLoadEvent, IDataTableColumn } from '../../../../shared/datatable/datatable.component';
import { IProduct } from '../../../../model/IProduct';
import 'rxjs/add/operator/map';

const DECIMAL_FORMAT: (v: any) => any = (v: number) => v.toFixed(2);

@Component({
  changeDetection: ChangeDetectionStrategy.OnPush,
  selector: 'app-products-list',
  templateUrl: './products-list.component.html',
  styleUrls: ['./products-list.component.scss']
})
export class ProductsListComponent implements OnInit {
  columns: IDataTableColumn[] = [
    { name: 'name',  label: 'Product name', sortable: true, width: 100 },
    { name: 'description', label: 'Description', filter: true, sortable: true, width: 300, nooverflow: true },
    { name: 'categoryName', label: 'Category', filter: true, sortable: true, width: 100 },
    { name: 'state', label: 'Status', filter: true, sortable: true, width: 120 },
    { name: 'vendorName', label: 'Vendor', filter: true, sortable: true, width: 80 },
    { name: 'price', label: 'Price', numeric: true, sortable: true, format: DECIMAL_FORMAT, width: 20 },
  ];

  loading: boolean = false;

  filteredData: IProduct[] = [];
  filteredTotal: number = this.filteredData.length;

  selectedRows: any[] = [];

  constructor(private dataTableService: TdDataTableService,
              private productService: ProductService,
              private cdr: ChangeDetectorRef) { }

  ngOnInit(): void {
    // initial loading with default state
    // if we would not do this we would get exception
    // - ExpressionChangedAfterItHasBeenCheckedError: Expression has changed after
    //   it was checked. Previous value: 'loading: false'. Current value: 'loading: true'.
    const loadVar: IDatatableLoadEvent = {
      searchTerm: '',
      fromRow: 1,
      currentPage: 1,
      pageSize: 10,
      sortBy: 'name',
      sortOrder: TdDataTableSortingOrder.Ascending
    };
    this.load(loadVar);
  }

  showAlert(message: string) {
    console.log(message);
  }

  load(loadParams: IDatatableLoadEvent): void {
    this.loading = true;
    this.productService.loalProducts(loadParams.sortBy, loadParams.currentPage, loadParams.pageSize, loadParams.searchTerm, loadParams.sortOrder.toLowerCase())
    .subscribe((res: HttpResponse<IProduct[]>) => {
      this.filteredData = res.body;

      // we have to set x-pagination to COSR rules on API server
      const xPagination = res.headers.get('x-pagination');
      this.filteredTotal = JSON.parse(xPagination).totalCount;

      this.loading = false;
      // Hack - because we are setting loading and until data are loaded
      // we have to stop changeDetection and tell angular when to detect changes
      // - this happens only when we are changing pagesize
      this.cdr.detectChanges();
    }, error => {
      this.loading = false;
      console.log(error);
    });
  }

}
