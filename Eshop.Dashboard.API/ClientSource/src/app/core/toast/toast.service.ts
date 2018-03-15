import { Injectable, Optional, SkipSelf } from '@angular/core';
import { Subject } from 'rxjs/Subject';

export interface ToastMessage {
  message: string;
}

@Injectable()
export class ToastService {

  // subject allows us to publish and subscribe in on object
  private toastSubject = new Subject<ToastMessage>();

  toastState = this.toastSubject.asObservable();

  constructor(@Optional() @SkipSelf() toast: ToastService) {
    if (toast) {
      console.log('Toast service already exists');
      return toast;
    } else {
      // TODO: add event logging service
      console.log('Created toast service');
    }
  }

  activate(message?: string) {
    this.toastSubject.next(<ToastMessage>{ message: message });
  }

}
