import { NgModule, Optional, SkipSelf } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';

import { throwIfAlreadyLoaded } from './module-import-guard';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';

import { UserService } from './user.service';
import { ToastModule } from './toast/toast.module';

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    BrowserAnimationsModule,
    HttpClientModule,
    ToastModule
  ],
  exports: [
    CommonModule,
    RouterModule,
    BrowserAnimationsModule,
    HttpClientModule,
    ToastModule
  ],
  declarations: [],
  providers: [
    UserService
  ]
})
export class CoreModule {
  // ensure it will be singletone and will not be reimported
  constructor( @Optional() @SkipSelf() parentModule: CoreModule) {
    throwIfAlreadyLoaded(parentModule, 'CoreModule');
  }
}
