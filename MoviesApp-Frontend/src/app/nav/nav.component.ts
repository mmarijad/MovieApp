import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../_services/user.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  model: any={};
  
  constructor(public userService: UserService, private router: Router) { }

  ngOnInit(): void {
  }

  login() {
    this.userService.login(this.model).subscribe(response => {
        this.router.navigateByUrl('/movies');
    }, error =>{
      console.log(error);
    });
  }

  logout() {
    this.userService.logout();
    this.router.navigateByUrl('register');
  }
}
