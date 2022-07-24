import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { ReplaySubject } from 'rxjs';
import {map} from 'rxjs/operators';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
//going to be used to make request to our api
 baseUrl='http://localhost:5020/';
 private currentUserSource = new ReplaySubject<User>(1);
 currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient, private router: Router) { }

  login(model: any){
    return this.http.post(this.baseUrl + 'account/login', model).pipe(
      map((response: User) =>{
        const user = response;
        if(user){
          localStorage.setItem('user', JSON.stringify(user)); //is going to populate our user inside localstorage in the browser
          this.currentUserSource.next(user);
        }
      }
      )
    )
  }

  register(model: any){
    this.router.navigateByUrl('/'); //navigate to the home page after we login
    return this.http.post(this.baseUrl + 'account/register', model).pipe(
      map((user: User)  => {
        if(user){
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUserSource.next(user);
        }
      })
    )
  }

  setCurrentUser(user: User){
    this.currentUserSource.next(user);
  }

  logout(){
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
  }
}
