import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ProductsComponent } from './products/products.component';
import { ProductsListComponent } from './products-list/products-list.component';

export const productsRoutes: Routes = [
  { path: '', component: ProductsComponent,
    children: [
      { path: 'all', component: ProductsListComponent }
    ]
  },
];

@NgModule({
  imports: [RouterModule.forChild(productsRoutes)],
  exports: [RouterModule],
})
export class ProductsRoutingModule { }
