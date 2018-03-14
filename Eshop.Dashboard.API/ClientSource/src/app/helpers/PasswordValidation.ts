import { AbstractControl, ValidatorFn } from '@angular/forms';

export class PasswordValidation {

  static MatchPassword(AC: AbstractControl) {
    const password = AC.get('password').value; // to get value in input tag
    const confirmPassword = AC.get('confirmPassword').value; // to get value in input tag
    if (password !== confirmPassword) {
      AC.get('confirmPassword').setErrors({ MatchPassword: true });
    } else {
      return null;
    }
  }

  static MatchValidator(element1name: string, element2name: string): ValidatorFn {
    return (control: AbstractControl): { [key: string]: any } => {
      const element1value = control.get(element1name).value; // to get value in input tag
      const element2value = control.get(element2name).value; // to get value in input tag
      if (element1value !== element2value) {
        control.get(element2name).setErrors({ MatchValidator: true });
      } else {
        return null;
      }
    };
  }
}
