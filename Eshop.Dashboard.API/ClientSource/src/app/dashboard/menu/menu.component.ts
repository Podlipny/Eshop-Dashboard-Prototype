import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { MenuService } from '../services/menu.service';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss']
})
export class MenuComponent implements OnInit {

  constructor(private router: Router, public menuService: MenuService) { }

  ngOnInit() {
  }

}
