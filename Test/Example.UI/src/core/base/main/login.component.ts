import { Component } from '@angular/core';
import { LoginBase } from 'codeshell/base-components';

@Component({ templateUrl: "./login.component.html" })
export class Login extends LoginBase {
    ForgotPasswordUrl = "/Auth/ForgotPassword";
}