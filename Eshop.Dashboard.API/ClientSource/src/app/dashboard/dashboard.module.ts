import { NgModule } from '@angular/core';

import { SharedModule } from '../shared/shared.module';

import { DashboardRoutingModule } from './dashboard.routing';
import { MainViewComponent } from './main-view/main-view.component';
import { DashboardComponent } from './dashboard.component';
import { UsersComponent } from './users/users.component';
import { ProductsComponent } from './products/products.component';
import { CustomentsComponent } from './customents/customents.component';

@NgModule({
  imports: [
    DashboardRoutingModule,
    SharedModule
  ],
  declarations: [
    DashboardComponent,
    MainViewComponent,
    UsersComponent,
    ProductsComponent,
    CustomentsComponent]
})
export class DashboardModule { }
