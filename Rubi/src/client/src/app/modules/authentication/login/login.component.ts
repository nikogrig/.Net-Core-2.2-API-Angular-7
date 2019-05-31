import { Component } from '@angular/core';
import { LoginModel } from 'src/app/models/auth.read.model';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';
import { first } from 'rxjs/operators';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent{
  
  model: LoginModel;
  
  constructor(private authService: AuthService, private router: Router) {
    this.model = new LoginModel('','');
  } 

  login(){
      this.authService
        .login(this.model)
        .pipe(first())
        .subscribe();
  }
}
