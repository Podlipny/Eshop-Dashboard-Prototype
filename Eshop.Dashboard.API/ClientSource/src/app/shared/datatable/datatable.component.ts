import { Component, EventEmitter, Input, Output, OnInit, AfterViewInit, SimpleChanges, ContentChild, TemplateRef } from '@angular/core';

import { TdDataTableSortingOrder, ITdDataTableColumnWidth } from '@covalent/core/data-table';
import { ITdDataTableColumn } from '@covalent/core/data-table';
import { ITdDataTableSortChangeEvent } from '@covalent/core/data-table';
import { IPageChangeEvent } from '@covalent/core/paging';

export interface IDatatableLoadEvent {
  searchTerm: string;
  fromRow: number;
  currentPage: number;
  pageSize: number;
  sortBy: string;
  sortOrder: TdDataTableSortingOrder;
}

export interface IDataTableColumn extends ITdDataTableColumn {
  name: string;
  label: string;
  tooltip?: string;
  numeric?: boolean;
  format?: (value: any) => any;
  nested?: boolean;
  sortable?: boolean;
  hidden?: boolean;
  filter?: boolean;
  width?: ITdDataTableColumnWidth | number;
  nooverflow?: boolean;
}

@Component({
  selector: 'app-datatable',
  templateUrl: './datatable.component.html',
  styleUrls: ['./datatable.component.scss']
})
export class DatatableComponent implements OnInit, AfterViewInit {
  @ContentChild(TemplateRef) templateRef: TemplateRef<any>;

  @Input() loading: boolean = false;
  @Input() data: any[] = [];
  @Input() totalCount: number = 0;
  @Input() columns: IDataTableColumn[] = [];

  @Input() sortable: boolean = false;
  @Input() selectable: boolean = false;
  @Input() clickable: boolean = false;
  @Input() multiple: boolean = false;

  // style inputs
  @Input() minHeight: number = 250;
  @Input() actionsVisible: boolean = false;
  @Input() actionsWidth: number = 90;

  @Output() loadEvent: EventEmitter<IDatatableLoadEvent> = new EventEmitter();
  @Output() rowClickEvent: EventEmitter<any> = new EventEmitter();
  @Output() selectedRowsChange: EventEmitter<boolean> = new EventEmitter<boolean>();

  searchTerm: string = '';
  fromRow: number = 1;
  currentPage: number = 1;
  pageSize: number = 10;
  sortBy: string = 'name';
  sortOrder: TdDataTableSortingOrder = TdDataTableSortingOrder.Ascending;

  selectedRows: any[] = [];

  constructor() { }

  ngOnInit() {
  }

  ngAfterViewInit() {
    if (this.columns.length !== 0) {
      this.sortBy = this.columns[0].name;
    }
  }

  checkAll(event: any) {
    if (this.selectedRows.length === this.data.length) {
      this.selectedRows = [];
    } else {
      this.selectedRows = Object.assign([], this.data);
    }
  }

  check(row: any) {
    const index: number = this.selectedRows.indexOf(row);
    if (index !== -1) {
      this.selectedRows.splice(index, 1);
    } else {
      this.selectedRows.push(row);
    }
  }

  click(event: any): void {
    this.rowClickEvent.emit(event);
  }

  sort(sortEvent: ITdDataTableSortChangeEvent): void {
    // empty selected rows
    this.selectedRows = [];

    if (this.sortBy !== sortEvent.name) {
      this.sortBy = sortEvent.name;
      this.sortOrder = TdDataTableSortingOrder.Ascending;
    } else if (this.sortOrder === TdDataTableSortingOrder.Ascending) {
      this.sortOrder = TdDataTableSortingOrder.Descending;
    } else {
      this.sortOrder = TdDataTableSortingOrder.Ascending;
    }
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

  filter(): void {
    const loadVar: IDatatableLoadEvent = {
      searchTerm: this.searchTerm,
      fromRow: this.fromRow,
      currentPage: this.currentPage,
      pageSize: this.pageSize,
      sortBy: this.sortBy,
      sortOrder: this.sortOrder
    };
    this.loadEvent.emit(loadVar);
  }
}
