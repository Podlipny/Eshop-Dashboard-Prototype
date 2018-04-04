import { Component, OnInit, Input, HostListener } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';

import { MenuService, MenuItem } from '../services/menu.service';
import { trigger, transition, animate, style } from '@angular/animations';

@Component({
  selector: 'app-menu-item',
  templateUrl: './menu-item.component.html',
  styleUrls: ['./menu-item.component.scss'],
  animations: [
    trigger('visibilityChanged', [
        transition(':enter', [ // :enter is alias to 'void => *' element jeste neni v DOM
            style({opacity: 0}),
            animate(250, style({opacity: 1}))
        ]),
        transition(':leave', [   // :leave is alias to '* => void' element mizi z DOM
            animate(100, style({opacity: 0}))
        ])
    ])
]
})
export class MenuItemComponent implements OnInit {
  @Input() item = <MenuItem>null;
  @Input() isParentItem: boolean = false;

  isActiveRoute = false;
  popupOpened = false;

  constructor(private router: Router, private menuService: MenuService) { }

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

  // HostListener will listen for click event on component
  @HostListener('click', ['$event'])
  onClick(event): void {
    event.stopPropagation();

    if (this.item.submenu) {
      this.popupOpened = !this.popupOpened; // open popup for vertical menu
    } else {
      this.router.navigate(['/' + this.item.route]);
    }
  }
}
