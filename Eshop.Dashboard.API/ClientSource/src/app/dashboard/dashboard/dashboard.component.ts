import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSidenav } from '@angular/material';

import { MenuService } from '../services/menu.service';
import { dashboardMenuItems } from '../dashboard.menu';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  @ViewChild(MatSidenav) sidenav: MatSidenav;

  constructor(private menuService: MenuService) { }

  ngOnInit() {
    this.menuService.items = dashboardMenuItems;
  }

}
