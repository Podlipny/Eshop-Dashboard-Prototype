import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators, FormBuilder } from '@angular/forms';
import { IUser } from '../../model/IUser';
import { AuthService } from '../auth.service';
import { ICredentials } from '../../model/ICredentials';
import { Router } from '@angular/router';
import { ToastService } from '../../core/toast/toast.service';
import { UserService } from '../../core/user.service';

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

  remembermeFormControl = new FormControl(false);

  requreAlert: string = 'This field is required';

  constructor(private authService: AuthService, 
              private userService: UserService, 
              private toastService: ToastService, 
              private router: Router) {
      // if (this.userService.isAuthorized()) {
      //   this.router.navigate(['/']);
      // }

      this.loginForm = new FormGroup({
      username: this.usernameFormControl,
      password: this.passwordFormControl,
      rememberme: this.remembermeFormControl
    });
   }

  ngOnInit() {
  }

  login(credentials: ICredentials) {
    // TODO: implement LoaderService to fire global loaders
    this.authService.login(credentials).subscribe((data: any) => {
      this.userService.authorize(data.token, Date.parse(data.expiration));
      this.userService.loadUser();
      this.router.navigate(['/']);
    }, error => {
   });
  }

}
