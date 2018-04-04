import { Component, OnInit, Input } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';

import { MenuService, MenuItem } from '../services/menu.service';

@Component({
  selector: 'app-menu-item',
  templateUrl: './menu-item.component.html',
  styleUrls: ['./menu-item.component.scss']
})
export class MenuItemComponent implements OnInit {
  @Input() item = <MenuItem>null;
  @Input() parentIsPopup = true;
  isActiveRoute = false;

  mouseInItem = false;
  mouseInPopup = false;
  popupLeft = 0;
  popupTop = 34;

  constructor(private router: Router, public menuService: MenuService) { }

  checkActiveRoute(route: string) {
    // tells us which item in menu is activeRoute
    this.isActiveRoute = (route === '/' + this.item.route);
  }

  ngOnInit() {
    // we do not have access to properties in constructor
    this.checkActiveRoute(this.router.url);

    // if we change route, we have to change active item
    this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd) {
        this.checkActiveRoute(event.url);
        // TODO: add logging
        }
    });
  }
}
