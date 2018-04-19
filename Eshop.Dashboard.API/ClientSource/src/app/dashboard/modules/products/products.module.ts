import { NgModule } from '@angular/core';

import { ProductsRoutingModule } from './products.routing';
import { SharedModule } from '../../../shared/shared.module';
import { CovalentModule } from '../../../shared/covalent.module';
import { MaterialModule } from '../../../shared/material.module';

import { ProductsComponent } from './products.component';
import { ProductsListComponent } from './products-list/products-list.component';
import { ProductDetailComponent } from './product-detail/product-detail.component';
import { VendorsListComponent } from './vendors-list/vendors-list.component';

@NgModule({
  imports: [
    ProductsRoutingModule,
    SharedModule,
    MaterialModule,
    CovalentModule
  ],
  declarations: [
    ProductsComponent,
    ProductsListComponent,
    ProductDetailComponent,
    VendorsListComponent
  ]
})
export class ProductsModule { }
