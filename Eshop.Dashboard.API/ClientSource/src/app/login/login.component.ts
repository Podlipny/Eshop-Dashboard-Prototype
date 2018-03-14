import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators, FormBuilder } from '@angular/forms';
import { IUser } from '../model/user';
import { UserService } from '../core/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  rememberme: boolean = false;

  usernameFormControl = new FormControl('', [
    Validators.required,
    Validators.maxLength(255),
  ]);

  passwordFormControl = new FormControl('', [
    Validators.required,
    Validators.minLength(6),
    Validators.maxLength(255),
  ]);

  requreAlert: string = 'This field is required';

  constructor(private _userservice: UserService) {
    this.loginForm = new FormGroup({username: this.usernameFormControl, password: this.passwordFormControl});
   }

  ngOnInit() {
  }

  login(user: IUser) {
    this._userservice.login(user, this.rememberme);
  }

}
