import { NgModule } from '@angular/core';

import { SharedModule } from '../shared/shared.module';
import { MaterialModule } from '../shared/material.module';
import { DashboardRoutingModule } from './dashboard.routing';

import { BoardViewComponent } from './boardview/boardview.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ToolbarComponent } from './toolbar/toolbar.component';

import { ProductsModule } from './modules/products/products.module';
import { UsersModule } from './modules/users/users.module';
import { ProductService } from './services/product.service';
import { MenuComponent } from './menu/menu.component';
import { MenuItemComponent } from './menu-item/menu-item.component';
import { MenuService } from './services/menu.service';
import { MenuExpanderComponent } from './menu-expander/menu-expander.component';
import { VendorsService } from './services/vendors.service';

@NgModule({
  imports: [
    DashboardRoutingModule,
    SharedModule,
    MaterialModule
  ],
  declarations: [
    ToolbarComponent,
    DashboardComponent,
    BoardViewComponent,
    MenuComponent,
    MenuItemComponent,
    MenuExpanderComponent
  ],
  providers: [ ProductService,
               MenuService, 
               VendorsService ]
})
export class DashboardModule { }
