// import { Component, OnInit, ChangeDetectionStrategy, AfterViewInit, ChangeDetectorRef } from '@angular/core';
// import { TdDataTableService, TdDataTableSortingOrder, ITdDataTableSortChangeEvent, ITdDataTableColumn } from '@covalent/core/data-table';
// import { IPageChangeEvent } from '@covalent/core/paging';
// import { ProductService } from '../../../services/product.service';
// import { IProduct } from '../../../../model/IProduct';
// import { ToastService } from '../../../../core/toast/toast.service';
// import { HttpResponse } from '@angular/common/http';
// import 'rxjs/add/operator/map';

// // TODO: clean up this
// const DECIMAL_FORMAT: (v: any) => any = (v: number) => v.toFixed(2);

// @Component({
//   selector: 'app-products-list',
//   templateUrl: './products-list.component.html',
//   styleUrls: ['./products-list.component.scss']
// })
// export class ProductsListComponent implements OnInit {
//   columns: ITdDataTableColumn[] = [
//     { name: 'name',  label: 'Product name', sortable: true, width: { min: 200, max: 300 } },
//     { name: 'description', label: 'Description', filter: true, sortable: true, width: { min: 300, max: 400 } },
//     { name: 'categoryName', label: 'Category', filter: true, sortable: true, width: { min: 100, max: 150 } },
//     { name: 'state', label: 'Status', filter: true, sortable: true, width: 120 },
//     { name: 'vendorName', label: 'Vendor', filter: true, sortable: true, width: { min: 110, max: 150 } },
//     { name: 'price', label: 'Price', numeric: true, sortable: true, format: DECIMAL_FORMAT, width: 100 },
//     { name: 'buttons', label: '', width: 100 }
//   ];

//   loading: boolean = false;

//   productData: IProduct[] = [];

//   filteredData: any[] = this.productData;
//   filteredTotal: number = this.productData.length;

//   searchTerm: string = '';
//   fromRow: number = 1;
//   currentPage: number = 1;
//   pageSize: number = 10;
//   sortBy: string = 'name';
//   selectedRows: any[] = [];
//   sortOrder: TdDataTableSortingOrder = TdDataTableSortingOrder.Ascending;

//   constructor(private _dataTableService: TdDataTableService,
//               private _productService: ProductService,
//               private cdr: ChangeDetectorRef) { }

//   ngOnInit(): void {
//     this.filter();
//   }

//   sort(sortEvent: ITdDataTableSortChangeEvent): void {
//     this.sortBy = sortEvent.name;
//     this.sortOrder = sortEvent.order;
//     this.filter();
//   }

//   search(searchTerm: string): void {
//     this.searchTerm = searchTerm;
//     this.filter();
//   }

//   page(pagingEvent: IPageChangeEvent): void {
//     this.fromRow = pagingEvent.fromRow;
//     this.currentPage = pagingEvent.page;
//     this.pageSize = pagingEvent.pageSize;
//     this.filter();
//   }

//   showAlert(event: any): void {
//     const row: any = event.row;
//     // .. do something with event.row
//   }

//   filter(): void {
//     this.loading = true;
//     this._productService.loalProducts(this.sortBy, this.currentPage, this.pageSize, this.searchTerm, this.sortOrder.toLowerCase())
//     .subscribe((res: HttpResponse<IProduct[]>) => {
//       this.productData = res.body;
//       this.filteredData = res.body;

//       // we have to set x-pagination to COSR rules on API server
//       const xPagination = res.headers.get('x-pagination');
//       this.filteredTotal = JSON.parse(xPagination).totalCount;
//       this.loading = false;
//     }, error => {
//       console.log(error.error);
//     });
//   }

// }


import { Component, OnInit, ChangeDetectionStrategy, AfterViewInit, ChangeDetectorRef } from '@angular/core';
import { TdDataTableService, TdDataTableSortingOrder, ITdDataTableSortChangeEvent } from '@covalent/core/data-table';
import { IPageChangeEvent } from '@covalent/core/paging';
import { ProductService } from '../../../services/product.service';
import { IProduct } from '../../../../model/product';
import { ToastService } from '../../../../core/toast/toast.service';
import { HttpResponse } from '@angular/common/http';
import 'rxjs/add/operator/map';
import { IDatatableLoadEvent, IDataTableColumn } from '../../../../shared/datatable/datatable.component';

// TODO: clean up this
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

  searchTerm: string = '';
  fromRow: number = 1;
  currentPage: number = 1;
  pageSize: number = 10;
  sortBy: string = 'name';
  selectedRows: any[] = [];
  sortOrder: TdDataTableSortingOrder = TdDataTableSortingOrder.Ascending;

  constructor(private _dataTableService: TdDataTableService,
              private _productService: ProductService,
              private _cdr: ChangeDetectorRef) { }

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
    this._productService.loalProducts(loadParams.sortBy, loadParams.currentPage, loadParams.pageSize, loadParams.searchTerm, loadParams.sortOrder.toLowerCase())
    .subscribe((res: HttpResponse<IProduct[]>) => {
      this.filteredData = res.body;

      // we have to set x-pagination to COSR rules on API server
      const xPagination = res.headers.get('x-pagination');
      this.filteredTotal = JSON.parse(xPagination).totalCount;

      this.loading = false;
      // Hack - because we are setting loading and until data are loaded
      // we have to stop changeDetection and tell angular when to detect changes
      // - this happens only when we are changing pagesize
    }, error => {
      console.log(error.error);
    });
  }

}
