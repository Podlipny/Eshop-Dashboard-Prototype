import { NgModule, Optional, SkipSelf } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToastService } from './toast.service';
import { ToastComponent } from './toast.component';
import { throwIfAlreadyLoaded } from '../module-import-guard';

@NgModule({
  imports: [ CommonModule ],
  exports: [ ToastComponent ],
  declarations: [ ToastComponent ],
  providers: [ ToastService ]
})
export class ToastModule {
  constructor(@Optional() @SkipSelf() parentModule: ToastModule) {
    throwIfAlreadyLoaded(parentModule, 'ToastModule')
  }
}
