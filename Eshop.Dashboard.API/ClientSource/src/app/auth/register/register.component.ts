import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';

import { IUser } from '../../model/IUser';
import { PasswordValidation } from '../../helpers/PasswordValidation';
import { ToastService } from '../../core/toast/toast.service';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;
  user: IUser;
  registrationError: string = null;

  username: string = '';
  firstname: string = '';
  lastname: string = '';
  password: string = '';
  confirmPassword: string = '';
  telephone: number = null;

  requireAlert: string = 'This field is required';
  matchPasswordAlert: string = 'Passwords must match!';

  constructor(private fb: FormBuilder,
              private authService: AuthService,
              private router: Router,
              private toastService: ToastService) {
    this.registerForm = fb.group({
      'username': [null, Validators.compose([Validators.required, Validators.maxLength(50)])],
      'firstname': [null, Validators.compose([Validators.required, Validators.maxLength(50)])],
      'lastname': [null, Validators.compose([Validators.required, Validators.maxLength(100)])],
      'password': [null, Validators.compose([Validators.required, Validators.minLength(6), Validators.maxLength(500)])],
      'confirmPassword': [null, Validators.compose([Validators.required, Validators.minLength(6), Validators.maxLength(500)])],
      'telephone': [null, Validators.compose([Validators.minLength(9), Validators.maxLength(12)])],
      'acceptTerms': [null, Validators.requiredTrue]
    }, {
      validator: PasswordValidation.MatchValidator('password', 'confirmPassword')
    });
  }

  ngOnInit() {
  }

  register(user: IUser) {
    // TODO: implement LoaderService to fire global loaders
    this.authService.register(user).subscribe(data => {
        this.router.navigate(['/']);
      }, error => {

    });
  }

}
