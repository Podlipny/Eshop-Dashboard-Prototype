import { NgModule } from '@angular/core';

import { SharedModule } from '../shared/shared.module';
import { AuthRoutingModule } from './auth.routing';
import { MaterialModule } from '../shared/material.module';

import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';

@NgModule({
  imports: [
    SharedModule,
    MaterialModule,
    AuthRoutingModule
  ],
  exports: [
  ],
  declarations: [
    LoginComponent,
    RegisterComponent
  ],
  providers: [ ]
})
export class AuthModule { }
