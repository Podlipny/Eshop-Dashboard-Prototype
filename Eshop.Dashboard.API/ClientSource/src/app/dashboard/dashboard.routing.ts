import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AuthGuard } from '../core/auth-guard.service';

import { MainViewComponent } from './main-view/main-view.component';
import { DashboardComponent } from './dashboard.component';
import { UsersComponent } from './users/users.component';
import { ProductsComponent } from './products/products.component';
import { CustomentsComponent } from './customents/customents.component';

export const dasboardRoutes: Routes = [
  { path: '', component: DashboardComponent, canActivate: [AuthGuard],
    children: [
      { path: '', component: MainViewComponent },
      { path: 'customers', component: CustomentsComponent },
      { path: 'products', component: ProductsComponent },
      { path: 'users', component: UsersComponent },
          // { path: 'dashboard', component: DashboardComponent },
          // { path: 'country-list/:count', component: CountryListComponent },
          // { path: 'country-detail/:id/:operation', component: CountryDetailComponent },
          // { path: 'country-maint', component: CountryMaintComponent },
          // { path: 'settings', component: SettingsComponent },
    ] },
];

@NgModule({
  imports: [RouterModule.forChild(dasboardRoutes)],
  exports: [RouterModule],
})
export class DashboardRoutingModule { }
