import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Error401Component } from './error401/error401.component';
import { Error403Component } from './error403/error403.component';
import { Error500Component } from './error500/error500.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { ErrorInterceptor } from 'src/app/interceptors/error.interceptor';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
  declarations: [
    Error401Component, 
    Error403Component, 
    Error500Component],
  imports: [
    HttpClientModule,
    ToastrModule.forRoot(),
    CommonModule,
    BrowserAnimationsModule,
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorInterceptor,
      multi: true
    }
  ],
  exports: []
})
export class ErrorModule { }
