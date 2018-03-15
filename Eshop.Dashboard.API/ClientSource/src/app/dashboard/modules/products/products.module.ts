import { NgModule } from '@angular/core';

import { AllProductsComponent } from './all-products/all-products.component';
import { ProductsRoutingModule } from './products.routing';
import { SharedModule } from '../../../shared/shared.module';
import { ProductsComponent } from './products/products.component';
import { CovalentModule } from '../../../shared/covalent.module';
import { MaterialModule } from '../../../shared/material.module';

@NgModule({
  imports: [
    ProductsRoutingModule,
    SharedModule,
    MaterialModule,
    CovalentModule
  ],
  declarations: [
    ProductsComponent,
    AllProductsComponent
  ]
})
export class ProductsModule { }
