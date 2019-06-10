import { Component, OnInit } from '@angular/core';
import { select, Store } from '@ngrx/store';
import * as userSelectors from '../../../store/selectors/users.selector';
import { UsersListModel } from 'src/app/models/read.model';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { AdminService } from 'src/app/services/admin.service';
import { AdminState } from 'src/app/store/admin.state';

@Component({
  selector: 'app-get-user',
  templateUrl: './get-user.component.html',
  styleUrls: ['./get-user.component.css']
})
export class GetUserComponent implements OnInit {

  users$: Observable<UsersListModel[]>;
  id: string;
  modalUser: UsersListModel;
  constructor(
    private toastr: ToastrService,
    private adminService: AdminService, 
    private store: Store<AdminState>) {
  }

  ngOnInit() {
    this.getUsers();
  }
  
  getUsers(){
    this.getListOfUsers();
  }

  getModalUser(userId){
    this.store.pipe(select(userSelectors.getItemById(userId)))
    .subscribe((item : UsersListModel) => {
      this.modalUser = item;
    });
  }

  deleteUser(){
    this.adminService
    .deleteUser(this.modalUser.id)
    .subscribe(() => {
      setTimeout(() => {
        //this.getListOfUsers();
      }, 500); 
      this.toastr.success(`${this.modalUser.username}`, 'Successfully deleted');
    })
  }

  private getListOfUsers(){
    this.adminService
    .getAllUsers()
    .subscribe(() => {
      this.users$ = this.store.pipe(select(state => state.users.all)); // working without pipe, because is observable
    });
  }
}
