import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Injectable({ providedIn: 'root' })
export class AuthenticationGuard implements CanActivate {

  constructor(private authService: AuthService, private router: Router){ 
  }

  canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    
    return this.checkIfLog();
  }

  checkIfLog() {
      if(this.authService.checkIfLoggedIn()) { 
      return true;
    }
    this.router.navigate(['/login']);
    localStorage.clear();  
    return false;
  }
}
