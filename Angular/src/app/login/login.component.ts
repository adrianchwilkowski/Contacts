import { Component, OnInit } from '@angular/core'
import { FormBuilder, FormControl, FormGroup, Validators,ReactiveFormsModule } from '@angular/forms'
import { IdentityService } from '../../services/identity.service'
import { Login } from '../../models/Identity/login'
import { Router} from '@angular/router'
import { HttpClient } from '@angular/common/http'
import { catchError, finalize, of, switchMap, tap } from 'rxjs'

@Component({
  templateUrl: './login.component.html',
  styles: [`
    em {float:right; color:#E05C65; padding-left: 10px;}
    .error input {background-color:#E3C3C5;}
    .error ::-webkit-input-placeholder { color: #999; }
    .error ::-moz-placeholder { color: #999; }
    .error :-moz-placeholder { color:#999; }
    .error :ms-input-placeholder { color: #999; }
  `],
  imports: [ReactiveFormsModule],
  standalone: true
})
export class LoginComponent implements OnInit{
profileForm: FormGroup;
message: string | null = null;

  constructor(private router:Router, private identityService: IdentityService,  private formBuilder: FormBuilder) {
    this.profileForm = this.formBuilder.group({
      login: [''],
      password: ['']
    });
  }
  ngOnInit(): void{
    this.profileForm = this.formBuilder.group({
      login: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  trySignIn() {
    if (this.profileForm.valid) {
      this.identityService.loginUser(this.profileForm.get('login')?.value, this.profileForm.get('password')?.value)
      .pipe(
        tap(() => {}),
        catchError((error) => {
          this.message = error.error;
          return of();
        })
      )
    .subscribe();
    }
  }

  cancel() {
    this.router.navigate([''])
  }
       
}