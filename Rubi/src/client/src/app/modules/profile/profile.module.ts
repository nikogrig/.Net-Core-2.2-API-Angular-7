import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProfileEditComponent } from './profile-edit/profile-edit.component';
import { UserProfileComponent } from './user-profile/user-profile.component';
import { ProfileService } from 'src/app/services/profile.service';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { ProfileInterceptor } from 'src/app/interceptors/profile.interceptor';

@NgModule({
  declarations: [ProfileEditComponent, UserProfileComponent],
  imports: [
    CommonModule
  ],
  providers: [
    ProfileService,
   {
     provide: HTTP_INTERCEPTORS,
     useClass: ProfileInterceptor,
      multi: true 
   }
  ],
  exports: [UserProfileComponent],
})
export class ProfileModule { }
