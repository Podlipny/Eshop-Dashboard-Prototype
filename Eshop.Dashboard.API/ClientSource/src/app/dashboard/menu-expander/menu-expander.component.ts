import { Component, OnInit, Input } from '@angular/core';
import { MenuItem } from '../services/menu.service';

@Component({
  selector: 'app-menu-expander',
  templateUrl: './menu-expander.component.html',
  styleUrls: ['./menu-expander.component.scss']
})
export class MenuExpanderComponent implements OnInit {
  @Input() menu: Array<MenuItem>;

  constructor() { }

  ngOnInit() {
  }

}
