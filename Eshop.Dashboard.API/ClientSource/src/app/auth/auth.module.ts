import { NgModule } from '@angular/core';

import { SharedModule } from '../shared/shared.module';
import { AuthRoutingModule } from './auth.routing';
import { MaterialModule } from '../shared/material.module';

import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { AuthService } from './auth.service';

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
  providers: [ AuthService ]
})
export class AuthModule { }
