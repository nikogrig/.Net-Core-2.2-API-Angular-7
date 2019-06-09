import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToastrModule } from 'ngx-toastr';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { StoreModule } from '@ngrx/store';
import { adminReducers } from 'src/app/store/index.admin-store';
import { AdminPanelComponent } from './admin-panel/admin-panel.component';
import { GetUserComponent } from './get-user/get-user.component';
import { UserDetailComponent } from './user-detail/user-detail.component';
import { EditUserComponent } from './edit-user/edit-user.component';
import { CreateUserComponent } from './create-user/create-user.component';
import { AdminService } from 'src/app/services/admin.service';
import { AdminInterceptor } from 'src/app/interceptors/admin.interceptor';
import { ClarityModule } from '@clr/angular';
import { SidenavHighLightDirective } from 'src/app/directives/sidenav.directive';

@NgModule({
  declarations: [
    AdminPanelComponent, 
    GetUserComponent, 
    UserDetailComponent, 
    EditUserComponent, 
    CreateUserComponent,
    SidenavHighLightDirective,
  ],
  imports: [
    CommonModule,
    StoreModule.forRoot(adminReducers),
    ClarityModule,
    FormsModule,
    ToastrModule.forRoot(),
    ReactiveFormsModule,
    BrowserModule,
    RouterModule,
    HttpClientModule,
    BrowserAnimationsModule,
  ],
  providers:[
    AdminService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AdminInterceptor,
      multi: true 
    }
   ],
  exports: [ GetUserComponent, 
    AdminPanelComponent, 
    UserDetailComponent, 
    EditUserComponent, 
    CreateUserComponent, 
    SidenavHighLightDirective ]
})
export class AdminPanelModule { }
