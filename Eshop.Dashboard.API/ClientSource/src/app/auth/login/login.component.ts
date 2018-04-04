import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators, FormBuilder } from '@angular/forms';
import { IUser } from '../../model/IUser';
import { UserService } from '../../core/user.service';
import { AuthService } from '../auth.service';
import { ICredentials } from '../../model/ICredentials';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;

  usernameFormControl = new FormControl('', [
    Validators.required,
    Validators.maxLength(255),
  ]);

  passwordFormControl = new FormControl('', [
    Validators.required,
    Validators.minLength(6),
    Validators.maxLength(255),
  ]);

  remembermeFormControl = new FormControl('');

  requreAlert: string = 'This field is required';

  constructor(private _userservice: UserService, private _authservice: AuthService, private _router: Router) {
    this.loginForm = new FormGroup({
      username: this.usernameFormControl,
      password: this.passwordFormControl,
      rememberme: this.remembermeFormControl
    });
   }

  ngOnInit() {
  }

  login(credentials: ICredentials) {
    this._authservice.login(credentials).subscribe(tokenData => {
      this._userservice.token = tokenData.token;
      this._userservice.tokenExpiration = tokenData.expiration;
      this._router.navigate(['/']);
    }, error => {
      // TODO: implement toasterService and loggerService
      console.log(error);
    });
  }

}
