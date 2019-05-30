import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ClarityModule } from '@clr/angular';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import { AuthService } from 'src/app/services/auth.service';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ChangePasswordComponent } from './change-password/change-password.component';
import { TokenInterceptor } from 'src/app/interceptors/token.interceptor';
import { ToastrModule } from 'ngx-toastr';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    LoginComponent, 
    RegisterComponent, 
    ChangePasswordComponent],
  imports: [
    ClarityModule,
    BrowserAnimationsModule,
    CommonModule,
    FormsModule,
    ToastrModule.forRoot(),
    ReactiveFormsModule,
    BrowserModule,
    RouterModule,
    HttpClientModule,
  ],
  providers:[
    AuthService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    }
  ],
  exports: [
  ],
})
export class AuthenticationModule { }
