import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from '../landing/home/home.component';

const routes: Routes = [
  { path:'', pathMatch: 'full', redirectTo: 'home' },
  //{ path: '', component: HomeComponent, pathMatch: 'full' },
  
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class RoutingModule { }
