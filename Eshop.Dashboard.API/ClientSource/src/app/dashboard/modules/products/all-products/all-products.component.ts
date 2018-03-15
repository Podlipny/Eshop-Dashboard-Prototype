import { Component, OnInit, ChangeDetectionStrategy, AfterViewInit, ChangeDetectorRef } from '@angular/core';
import { TdDataTableService, TdDataTableSortingOrder, ITdDataTableSortChangeEvent, ITdDataTableColumn } from '@covalent/core/data-table';
import { IPageChangeEvent } from '@covalent/core/paging';
import { ProductService } from '../../../services/product.service';
import { IProduct } from '../../../../model/IProduct';
import { ToastService } from '../../../../core/toast/toast.service';

const DECIMAL_FORMAT: (v: any) => any = (v: number) => v.toFixed(2);

@Component({
  changeDetection: ChangeDetectionStrategy.OnPush,
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

  productData: IProduct[] = [];

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
    this._productService.loalAllProducts().subscribe(data => {
      this.productData = data;
      this.filter();
      // we are manually telling change detection strategy to change changes, because
      // AllProductsComponent works with empty product array[], but before rendering window, we
      // will load data
      this.cdr.detectChanges();
    }, error => {
      console.log(error.error);
    });
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
    let newData: any[] = this.productData;
    const excludedColumns: string[] = this.columns
    .filter((column: ITdDataTableColumn) => {
      return ((column.filter === undefined && column.hidden === true) ||
              (column.filter !== undefined && column.filter === false));
    }).map((column: ITdDataTableColumn) => {
      return column.name;
    });
    newData = this._dataTableService.filterData(newData, this.searchTerm, true, excludedColumns);
    this.filteredTotal = newData.length;
    newData = this._dataTableService.sortData(newData, this.sortBy, this.sortOrder);
    newData = this._dataTableService.pageData(newData, this.fromRow, this.currentPage * this.pageSize);
    this.filteredData = newData;
  }

}
