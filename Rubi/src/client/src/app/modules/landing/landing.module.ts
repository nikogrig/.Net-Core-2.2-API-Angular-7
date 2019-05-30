import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavigationMenuComponent } from './navigation-menu/navigation-menu.component';
import { HomeComponent } from './home/home.component';
import { ClarityModule } from '@clr/angular';
import { RouterModule } from '@angular/router';
import { JumbotronComponent } from './jumbotron/jumbotron.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AuthService } from 'src/app/services/auth.service';

@NgModule({
  declarations: [
    NavigationMenuComponent,
    HomeComponent,
    JumbotronComponent
  ],
  imports: [
    NgbModule,
    CommonModule,
    RouterModule,
    ClarityModule,
    HttpClientModule,
    BrowserModule,
    BrowserAnimationsModule
  ],
  providers:[
    AuthService,
  ]
  ,exports: [
    NgbModule,
    ClarityModule,
    NavigationMenuComponent,
    HomeComponent,
    JumbotronComponent
  ],
})
export class LandingModule { }
