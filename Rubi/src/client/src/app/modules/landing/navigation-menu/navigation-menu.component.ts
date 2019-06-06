import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-navigation-menu',
  templateUrl: './navigation-menu.component.html',
  styleUrls: ['./navigation-menu.component.css']
})
export class NavigationMenuComponent {

  constructor(private router: Router, 
    private authService: AuthService, 
    private toastr: ToastrService) { }

    getId() : string{
      if (this.authService.checkIfLoggedIn()) {
        let id = JSON.parse(localStorage.getItem('currentUser')).id
        return id;
      }
      return '';
    }
  
    logout() {
      this.authService
          .logout()
          .subscribe();
          this.router.navigate(['/login']);
          this.toastr.success(`You successfully loged out!` , 'Come back later')
          localStorage.clear();
          this.authService.authtoken = "";
    }

}
