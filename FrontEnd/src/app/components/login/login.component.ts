import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthenticationService } from 'src/app/services/authentication/authentication.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  LoginForm: FormGroup;

  constructor(
    private FormBuilder: FormBuilder,
    private AuthenticationService: AuthenticationService
  ) { }

  ngOnInit(): void {
    this.LoginForm = this.FormBuilder.group({
      tenant: [''],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]]
    });
  }

  submit() {
    const { email, password, tenant } = this.LoginForm.value;

    this.AuthenticationService.login(email, password, tenant);
  }

}
