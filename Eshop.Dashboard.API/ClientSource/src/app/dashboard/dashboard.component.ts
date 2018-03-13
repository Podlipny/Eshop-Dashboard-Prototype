import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-content',
  template: `
  <app-sidenav>
    <router-outlet></router-outlet>
  </app-sidenav>`
})
export class DashboardComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
