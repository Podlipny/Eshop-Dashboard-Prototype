import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MaterialModule } from './material.module';
import { CovalentModule } from './covalent.module';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { SideNavComponent } from './sidenav/sidenav.component';
import { DatatableComponent } from './datatable/datatable.component';

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    MaterialModule,
    CovalentModule
  ],
  exports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    SideNavComponent,
    DatatableComponent
  ],
  declarations: [SideNavComponent, DatatableComponent]
})
export class SharedModule { }
