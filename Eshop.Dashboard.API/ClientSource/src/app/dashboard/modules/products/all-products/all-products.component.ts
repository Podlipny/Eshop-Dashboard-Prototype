import { Component, OnInit, ChangeDetectionStrategy, AfterViewInit, ChangeDetectorRef } from '@angular/core';
import { TdDataTableService, TdDataTableSortingOrder, ITdDataTableSortChangeEvent, ITdDataTableColumn } from '@covalent/core/data-table';
import { IPageChangeEvent } from '@covalent/core/paging';
import { ProductService } from '../../../services/product.service';
import { IProduct } from '../../../../model/IProduct';
import { ToastService } from '../../../../core/toast/toast.service';
import { HttpResponse } from '@angular/common/http';
import 'rxjs/add/operator/map';

const DECIMAL_FORMAT: (v: any) => any = (v: number) => v.toFixed(2);

@Component({
  selector: 'app-all-products',
  templateUrl: './all-products.component.html',
  styleUrls: ['./all-products.component.scss']
})
export class AllProductsComponent implements OnInit {
  columns: ITdDataTableColumn[] = [
    { name: 'name',  label: 'Product name', sortable: true, width: 300 },
    { name: 'description', label: 'Description', filter: true, sortable: false },
    { name: 'price', label: 'Price', numeric: true, sortable: true, format: DECIMAL_FORMAT },
  ];

  productData = [];

  filteredData: any[] = this.productData;
  filteredTotal: number = this.productData.length;

  searchTerm: string = '';
  fromRow: number = 1;
  currentPage: number = 1;
  pageSize: number = 10;
  sortBy: string = 'name';
  selectedRows: any[] = [];
  sortOrder: TdDataTableSortingOrder = TdDataTableSortingOrder.Ascending;

  constructor(private _dataTableService: TdDataTableService,
              private _productService: ProductService,
              private cdr: ChangeDetectorRef) { }

  ngOnInit(): void {
    this.filter();
  }

  sort(sortEvent: ITdDataTableSortChangeEvent): void {
    this.sortBy = sortEvent.name;
    this.sortOrder = sortEvent.order;
    this.filter();
  }

  search(searchTerm: string): void {
    this.searchTerm = searchTerm;
    this.filter();
  }

  page(pagingEvent: IPageChangeEvent): void {
    this.fromRow = pagingEvent.fromRow;
    this.currentPage = pagingEvent.page;
    this.pageSize = pagingEvent.pageSize;
    this.filter();
  }

  showAlert(event: any): void {
    const row: any = event.row;
    // .. do something with event.row
  }

  filter(): void {
    this._productService.loalProducts(this.sortBy, this.currentPage, this.pageSize, this.searchTerm, this.sortOrder.toLowerCase())
    .subscribe((res: HttpResponse<IProduct[]>) => {
      this.productData = res.body;
      this.filteredData = res.body;

      // we have to set x-pagination to COSR rules on API server
      const xPagination = res.headers.get('x-pagination');
      this.filteredTotal = JSON.parse(xPagination).totalCount;

    }, error => {
      console.log(error.error);
    });
  }

}
