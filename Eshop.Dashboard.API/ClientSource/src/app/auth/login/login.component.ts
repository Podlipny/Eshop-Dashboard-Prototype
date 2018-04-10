import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators, FormBuilder } from '@angular/forms';
import { IUser } from '../../model/IUser';
import { UserService } from '../../core/user.service';
import { AuthService } from '../auth.service';
import { ICredentials } from '../../model/ICredentials';
import { Router } from '@angular/router';
import { ToastService } from '../../core/toast/toast.service';

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

  constructor(private userservice: UserService, private authservice: AuthService, private toastService: ToastService, private router: Router) {
    this.loginForm = new FormGroup({
      username: this.usernameFormControl,
      password: this.passwordFormControl,
      rememberme: this.remembermeFormControl
    });
   }

  ngOnInit() {
  }

  login(credentials: ICredentials) {
    this.authservice.login(credentials).subscribe(tokenData => {
      this.userservice.token = tokenData.token;
      this.userservice.tokenExpiration = tokenData.expiration;
      this.router.navigate(['/']);
    }, error => {
      // TODO: implement loggerService
      this.toastService.activate(error);
      console.log(error);
    });
  }

}
