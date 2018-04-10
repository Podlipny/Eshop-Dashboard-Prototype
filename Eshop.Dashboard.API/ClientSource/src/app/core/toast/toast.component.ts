import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs/Subscription';
import { ToastService } from './toast.service';

@Component({
  moduleId: module.id,
  selector: 'app-toast',
  templateUrl: './toast.component.html',
  styleUrls: ['./toast.component.scss']
})
export class ToastComponent implements OnInit, OnDestroy {

  private defaults = {
    title: '',
    message: ''
  };
  private toastElement: any;
  private toastSubscription: Subscription;

  title: string;
  message: string;

  constructor(private toastService: ToastService) {
    this.toastSubscription = this.toastService.toastState.subscribe((toastMessage) => {
      console.log(`Showing toast: ${toastMessage.message}`);
      this.activate(toastMessage.message);
    });
  }

  activate(message = this.defaults.message, title = this.defaults.title) {
    this.title = title;
    this.message = message;
    this.show();
  }

  ngOnInit() {
    this.toastElement = document.getElementById('toast');
  }

  ngOnDestroy() {
    this.toastSubscription.unsubscribe();
  }

  private show() {
    this.toastElement.style.opacity = 1;
    this.toastElement.style.zIndex = 9999;

    window.setTimeout(() => this.hide(), 2500);
  }

  private hide() {
    this.toastElement.style.opacity = 0;
    window.setTimeout(() => this.toastElement.style.zIndex = 0, 400);
  }

}
