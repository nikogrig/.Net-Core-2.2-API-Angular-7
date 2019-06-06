import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from '../authentication/login/login.component';
import { AuthenticationGuard } from 'src/app/guards/authentication.guard';
import { AuthorizationGuard } from 'src/app/guards/authorization.guard';
import { RegisterComponent } from '../authentication/register/register.component';
import { ChangePasswordComponent } from '../authentication/change-password/change-password.component';
import { Error401Component } from '../error/error401/error401.component';
import { Error403Component } from '../error/error403/error403.component';
import { Error500Component } from '../error/error500/error500.component';
import { AdminPanelComponent } from '../admin-panel/admin-panel/admin-panel.component';
import { GetUserComponent } from '../admin-panel/get-user/get-user.component';
import { UserDetailComponent } from '../admin-panel/user-detail/user-detail.component';
import { EditUserComponent } from '../admin-panel/edit-user/edit-user.component';
import { UserProfileComponent } from '../profile/user-profile/user-profile.component';
import { ProfileEditComponent } from '../profile/profile-edit/profile-edit.component';

const routes: Routes = [
  { path:'', pathMatch: 'full', redirectTo: 'home' },
  { path:'login', component: LoginComponent },
  { path:'register', component: RegisterComponent },
  { path:'change-password', component: ChangePasswordComponent },
  { path:'error/401', component: Error401Component },
  { path:'error/403', component: Error403Component },
  { path:'error/500', component: Error500Component },
  { path:'admin/panel', children: [
      { path: '', component: AdminPanelComponent },
      { path:'all-users', component: GetUserComponent }, 
      { path:'user-detail/:id', component: UserDetailComponent },
      { path:'edit-user/:id', component: EditUserComponent }
    ], canActivate: [AuthorizationGuard] },
  { path:'profile/:id', children: [
      { path: '', component: UserProfileComponent },
      { path: 'edit', component: ProfileEditComponent },
    ] , canActivate: [AuthenticationGuard] }   
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers:[AuthorizationGuard, AuthenticationGuard]
})
export class RoutingModule { }
