import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { UserService } from '../_services/user.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  token: string;
  
  constructor(private userService: UserService) { }

  canActivate(): Observable<boolean> {
    return this.userService.currentUser$.pipe(
        map(user => {
            if (user) { return true; }
            return false;
        })
    );
  }
}