import { Injectable } from '@angular/core';
import { environment } from '../config/environment';
import { ApiPaths } from '../config/apiPaths';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Login } from '../models/Identity/login';
import { Observable, catchError, tap, throwError } from 'rxjs';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class IdentityService {
  private readonly tokenKey = 'auth_token';
  private readonly isLoggedIn = 'is_logged_in';
  private readonly apiUrl = environment.baseUrl + ApiPaths.Identity;
  constructor(private httpClient: HttpClient,private router: Router) { }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  setToken(token: string): void {
    localStorage.setItem(this.tokenKey, token);
  }

  removeToken(): void {
    localStorage.removeItem(this.tokenKey);
  }

  login(loginModel:Login) {
    
    return this.httpClient.put<any>(`${this.apiUrl}/Login`, loginModel, {responseType: 'text' as 'json'}).pipe(
      tap((result: any) => {
        this.setToken(result);
      }),
      catchError((error) => {
        console.error('Błąd logowania:', error);
        return throwError(()=>error); 
      })
    );
  }

 loginUser(login: string, password: string) :Observable<void> {
  return new Observable<void>((observer) => {
    this.logout();
    var loginModel:Login = {
      login :login,
      password : password
    }
    this.login(loginModel).subscribe(() => {
      if (this.getToken() !== null) {
        localStorage.setItem(this.isLoggedIn, "true");
        this.router.navigate(['/']).then(() => {
          window.location.reload();
        });
      }
    })
    });
  }
  
  isLogged(): boolean{
    if(localStorage.getItem(this.isLoggedIn) == "true"){
      return true;
    }
    return false;
  }
  
  logout(): void{
    localStorage.removeItem(this.isLoggedIn);
    this.removeToken();
  }
}