import { Component, OnInit, ChangeDetectorRef, ChangeDetectionStrategy } from '@angular/core';
import { IDataTableColumn, IDatatableLoadEvent } from '../../../../shared/datatable/datatable.component';
import { IVendor } from '../../../../model/IVendor';
import { VendorsService } from '../../../services/vendors.service';
import { HttpResponse } from '@angular/common/http';
import { TdDataTableSortingOrder } from '@covalent/core/data-table';

@Component({
  changeDetection: ChangeDetectionStrategy.OnPush,
  selector: 'app-vendors-list',
  templateUrl: './vendors-list.component.html',
  styleUrls: ['./vendors-list.component.scss']
})
export class VendorsListComponent implements OnInit {
  columns: IDataTableColumn[] = [
    { name: 'name',  label: 'Product name', sortable: true, width: 200, nooverflow: true  },
    { name: 'ico', label: 'ICO', filter: true, sortable: true, width: 50},
    { name: 'dic', label: 'DIC', filter: true, sortable: true, width: 50 },
    { name: 'director', label: 'Status', filter: true, sortable: true, width: 100 },
    { name: 'contact.address1', label: 'Address', filter: true, sortable: true, nested: true, width: 150, nooverflow: true },
    { name: 'contact.psc', label: 'ZIP', filter: true, sortable: true, nested: true, width: 50 },
    { name: 'contact.city', label: 'City', filter: true, sortable: true, nested: true, width: 80 },
    { name: 'contact.state', label: 'State', filter: true, sortable: true, nested: true, width: 80 },
  ];

  loading: boolean = false;

  filteredData: IVendor[] = [];
  filteredTotal: number = this.filteredData.length;

  selectedRows: any[] = [];
  
  constructor(private vendorService: VendorsService,
              private cdr: ChangeDetectorRef) { }

  ngOnInit() {
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

  deleteVendor(vendorId: string) {
    this.loading = true;
  }

  load(loadParams: IDatatableLoadEvent): void {
    this.loading = true;
    this.vendorService.loalAll(loadParams.sortBy, loadParams.currentPage, loadParams.pageSize, loadParams.searchTerm, loadParams.sortOrder.toLowerCase())
    .subscribe((res: HttpResponse<IVendor[]>) => {
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
    });
  }

}
