import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Injectable({ providedIn: 'root' })
export class AuthorizationGuard implements CanActivate {

  constructor(private authService: AuthService, private router: Router) {
  }

  canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot)
   : boolean {

    return this.checkIflogedUserIsAdminRole();
  }

  checkIflogedUserIsAdminRole(){
        if(this.authService.checkIfUserIsAdmin()) {
          return true;
        }
     
        this.router.navigate(['/login']);
        localStorage.clear();       
        return false;
      }
}
