import { Component } from '@angular/core';
import { RegisterModel } from 'src/app/models/read.model';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  model: RegisterModel;
  registerFailed: boolean;
  errorMessage: string;
  
  constructor(private authService: AuthService) { 
    this.model = new RegisterModel('','','','','','', '', '','');
  }

  register() {
    console.log(this.model)
    delete this.model['confirmPassword'];
    this.authService
        .register(this.model)
        .subscribe(data => { this.model = data });      
  }

}
