import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ProductsComponent } from './products/products.component';
import { AllProductsComponent } from './all-products/all-products.component';

export const productsRoutes: Routes = [
  { path: '', component: ProductsComponent,
    children: [
      { path: 'all', component: AllProductsComponent }
    ]
  },
];

@NgModule({
  imports: [RouterModule.forChild(productsRoutes)],
  exports: [RouterModule],
})
export class ProductsRoutingModule { }
