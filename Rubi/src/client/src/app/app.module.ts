import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';
import { ClarityModule } from '@clr/angular';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LandingModule } from './modules/landing/landing.module';
import { RoutingModule } from './modules/routing/routing.module';
import { AuthenticationModule } from './modules/authentication/authentication.module';
import { ErrorModule } from './modules/error/error.module';
import { ProfileModule } from './modules/profile/profile.module';
import { AdminPanelModule } from './modules/admin-panel/admin-panel.module';

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    LandingModule,
    FormsModule,
    RoutingModule,
    AuthenticationModule,
    AdminPanelModule,
    ErrorModule,
    ClarityModule,
    ProfileModule,
    BrowserAnimationsModule
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule { }
