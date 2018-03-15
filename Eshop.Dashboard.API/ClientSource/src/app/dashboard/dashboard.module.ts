import { NgModule } from '@angular/core';

import { SharedModule } from '../shared/shared.module';
import { MaterialModule } from '../shared/material.module';
import { DashboardRoutingModule } from './dashboard.routing';

import { BoardViewComponent } from './boardview/boardview.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ToolbarComponent } from './toolbar/toolbar.component';

import { ProductsModule } from './modules/products/products.module';
import { UsersModule } from './modules/users/users.module';


@NgModule({
  imports: [
    DashboardRoutingModule,
    SharedModule,
    MaterialModule
  ],
  declarations: [
    ToolbarComponent,
    DashboardComponent,
    BoardViewComponent
  ]
})
export class DashboardModule { }
