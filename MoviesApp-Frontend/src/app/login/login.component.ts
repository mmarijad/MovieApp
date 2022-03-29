import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../_services/user.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  validationErrors: string[] = [];

  constructor(  private userService: UserService, 
                private formBuilder: FormBuilder, 
                private router: Router, 
                private toastr: ToastrService) {}

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm(){
    this.loginForm = this.formBuilder.group({
      username: ['', Validators.required],
      password:['', [Validators.required, Validators.minLength(6), Validators.maxLength(30)]],
    })
  }

  login(){
    this.userService.login(this.loginForm.value).subscribe(response => {
      this.router.navigateByUrl('/home');
    }, error => {
      this.validationErrors = error;
      console.log(error);                    
      if (error.status === 400)  
        {this.toastr.error('Pogrešno korisničko ime ili lozinka.');
      }
    });
  }
}
