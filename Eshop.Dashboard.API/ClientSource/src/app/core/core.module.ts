import { NgModule, Optional, SkipSelf } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';

import { throwIfAlreadyLoaded } from './module-import-guard';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    RouterModule,
    BrowserAnimationsModule
],
  exports: [
    CommonModule,
    FormsModule,
    RouterModule,
    BrowserAnimationsModule
  ],
  declarations: []
})
export class CoreModule {
  // ensure it will be singletone
  constructor( @Optional() @SkipSelf() parentModule: CoreModule) {
    throwIfAlreadyLoaded(parentModule, 'CoreModule');
  }
}
