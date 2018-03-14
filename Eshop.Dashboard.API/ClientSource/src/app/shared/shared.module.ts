import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialModule } from './material.module';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { SideNavComponent } from './sidenav/sidenav.component';

@NgModule({
  imports: [
    MaterialModule
  ],
  exports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    SideNavComponent
  ],
  declarations: [SideNavComponent]
})
export class SharedModule { }
