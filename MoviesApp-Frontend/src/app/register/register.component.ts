import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from '../_services/user.service';
import { ToastrService } from 'ngx-toastr';
import { ListService } from '../_services/list.service';
import { List } from '../_models/List';
import { JwtHelperService } from '@auth0/angular-jwt';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;
  listForm: FormGroup;
  validationErrors: string[] = [];
  list: List = new List();
  jwtHelper = new JwtHelperService();

  constructor(private userService: UserService, private formBuilder: FormBuilder, 
    private router: Router, private toastr: ToastrService, private listService: ListService) { }

  ngOnInit(): void {
    this.listForm = this.formBuilder.group({
      title: ['Favoriti', Validators.required],
      description: ['Omlijeni filmovi', Validators.required],
      userId: ['', Validators.required],
    });
    this.initializeForm();
  }

  initializeForm(){
    this.registerForm = this.formBuilder.group({
      username: ['', Validators.required],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      password:['', [Validators.required, Validators.minLength(8), Validators.maxLength(30)]],
      confirmPassword: ['', [Validators.required, this.matchValues('password')]],
    })
    this.registerForm.controls.password.valueChanges.subscribe(() => {
      this.registerForm.controls.confirmPassword.updateValueAndValidity();
    })
  }

  matchValues(matchTo: string): ValidatorFn{
    return(control: AbstractControl) => {
      return control?.value === control?.parent?.controls[matchTo].value ? null : { isMatching: true};
    }
  }

  register(){
    this.userService.register(this.registerForm.value).subscribe(response => {
      console.log(response);
      
      this.listForm.value.userId = this.userService.decodedToken?.unique_name;
      this.addList();
      this.router.navigateByUrl('/home');
    }, error => {
      this.validationErrors = error;
      if (error.status === 400)  
      {this.toastr.error('Korisnik već postoji.');
    }
    });
  }

  addList(){
    this.listService.addList(this.listForm.value, this.listForm.value.userId).subscribe(response => {});
  }
}