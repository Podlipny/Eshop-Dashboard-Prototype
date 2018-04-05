import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ProductsComponent } from './products/products.component';
import { ProductsListComponent } from './products-list/products-list.component';
import { ProductDetailComponent } from './product-detail/product-detail.component';

export const productsRoutes: Routes = [
  { path: '', component: ProductsComponent,
    children: [
      { path: '', component: ProductsListComponent },
      { path: 'add', component: ProductDetailComponent },
      { path: ':id/:operation', component: ProductDetailComponent }
    ]
  },
];

@NgModule({
  imports: [RouterModule.forChild(productsRoutes)],
  exports: [RouterModule],
})
export class ProductsRoutingModule { }
