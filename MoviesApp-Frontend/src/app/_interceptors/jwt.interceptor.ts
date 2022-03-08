import { Injectable } from '@angular/core';
import {HttpRequest,HttpHandler,HttpEvent,HttpInterceptor} from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserService } from '../_services/user.service';
import { User } from '../_models/user';
import { take } from 'rxjs/operators';
import { environment } from 'src/environments/environment';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  
  baseUrl: string = environment.baseUrl + 'api/';
  constructor(private userService: UserService) {}

  private isValidUrl(url: string): boolean{
    return url === this.baseUrl;
  }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    let currentUser: User;
    this.userService.currentUser$.pipe(take(1)).subscribe(user => currentUser = user);

    //Checking for current user and only for urls that are pointed toward out API
    if(currentUser && this.isValidUrl(request.url.slice(0, this.baseUrl.length))){
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${currentUser.token['result']}`
        }
      })
    }
    
    return next.handle(request);
  }
}