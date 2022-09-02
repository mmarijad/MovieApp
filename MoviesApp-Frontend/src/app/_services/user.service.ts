import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { User } from '../_models/user';
import { map } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ReplaySubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private baseUrl: string = environment.baseUrl + 'api/';
  private currentUserSource = new ReplaySubject<User>(1);
  currentUser$ = this.currentUserSource.asObservable();
  userToSave: User;
  name: string;
  lastname: string;
  jwtHelper = new JwtHelperService();
 
  jwtToken = localStorage.getItem("token");
  decodedToken = this.jwtHelper.decodeToken(this.jwtToken);

  constructor(private http: HttpClient) { }

  register(model: User) {
    return this.http.post(this.baseUrl + 'Users/register', model).pipe(
      map((response: User) => {
        const user = response;
        if (user) {
          this.setCurrentUser(user);
        } 
      })
    );
  }

  login(model: User) {
    return this.http.post(this.baseUrl + 'Users/login', model).pipe(
      map((response: User) => {
        const user = response;
        if (user) {
          this.setCurrentUser(user);
        }
      })
    )
  }

  setCurrentUser(user: User) {
    localStorage.setItem('token', JSON.stringify(user['token']['result']));
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUserSource.next(user);
    this.jwtToken = localStorage.getItem("token");
    this.decodedToken = this.jwtHelper.decodeToken(this.jwtToken);
 
    user.username = this.decodedToken.unique_name;
    user.name = this.decodedToken.given_name;
    user.lastname = this.decodedToken.family_name;
    user.id = this.decodedToken.nameid;
  }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
    this.jwtToken = "";
    this.decodedToken = "";
  }
}