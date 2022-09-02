import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { UserService } from '../_services/user.service';
import { Router } from '@angular/router';
import { User } from '../_models/User'

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  validationErrors: string[] = [];

  constructor(private userService: UserService, private formBuilder: FormBuilder, private router: Router) { }

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm(){
    this.loginForm = this.formBuilder.group({
      username: ['', Validators.required],
      password:['', [Validators.required, Validators.minLength(4), Validators.maxLength(10)]],
    })
  }

  login(){
    this.userService.login(this.loginForm.value).subscribe(response => {
      this.router.navigateByUrl('/home');
    }, error => {
      this.validationErrors = error;
      console.log(error);
    })
  }
}
