import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialModule } from './material.module';

import { SideNavComponent } from './sidenav/sidenav.component';

@NgModule({
  imports: [
    MaterialModule
  ],
  exports: [
    MaterialModule,
    SideNavComponent
  ],
  declarations: [SideNavComponent]
})
export class SharedModule { }
