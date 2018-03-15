import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AuthGuard } from '../core/auth-guard.service';

import { BoardViewComponent } from './boardview/boardview.component';
import { DashboardComponent } from './dashboard/dashboard.component';

export const dasboardRoutes: Routes = [
  { path: '', component: DashboardComponent, canActivate: [AuthGuard],
    children: [
      { path: '', component: BoardViewComponent },
      { path: 'settings', loadChildren: 'app/dashboard/modules/settings/settings.module#SettingsModule' },
      { path: 'customers', loadChildren: 'app/dashboard/modules/customers/customers.module#CustomersModule' },
      { path: 'products', loadChildren: 'app/dashboard/modules/products/products.module#ProductsModule' },
      { path: 'users', loadChildren: 'app/dashboard/modules/users/users.module#UsersModule' }
    ] },
];

@NgModule({
  imports: [RouterModule.forChild(dasboardRoutes)],
  exports: [RouterModule],
})
export class DashboardRoutingModule { }
