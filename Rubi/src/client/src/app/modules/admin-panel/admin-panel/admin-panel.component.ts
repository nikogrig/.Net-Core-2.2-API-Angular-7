import { Component } from '@angular/core';
import { AdminService } from 'src/app/services/admin.service';
import { Observable } from 'rxjs';
import { UsersListModel } from 'src/app/models/read.model';
import { Store, select } from '@ngrx/store';
import { AdminState } from 'src/app/store/admin.state';

@Component({
  selector: 'app-admin-panel',
  templateUrl: './admin-panel.component.html',
  styleUrls: ['./admin-panel.component.css']
})
export class AdminPanelComponent {

  users$: Observable<UsersListModel[]>;
  private showAllUsersFlag: boolean = false;
  private showCreateUserFlag: boolean = false;

  constructor(private adminService: AdminService,
    private store: Store<AdminState>) { }

  private getAllUsers(){
    this.getListOfUsers();
    this.showAllUsersFlag = !this.showAllUsersFlag;
    this.showCreateUserFlag = false;
  }

  private createUser(){
    this.showAllUsersFlag = false;
    this.showCreateUserFlag = !this.showCreateUserFlag;
  }

  private getListOfUsers(){ // TODO get user from stor without call server
    this.adminService
    .getAllUsers()
    .subscribe(() => {
      this.users$ = this.store.pipe(select(state => state.users.all)); // working without pipe, because is observable
    });
  }

}

