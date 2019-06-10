import { Component, OnInit } from '@angular/core';
import { UsersListModel } from 'src/app/models/read.model';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AdminService } from 'src/app/services/admin.service';
import { AdminState } from 'src/app/store/admin.state';
import { Store, select } from '@ngrx/store';
import * as userSelectors from '../../../store/selectors/users.selector';

@Component({
  selector: 'app-edit-user',
  templateUrl: './edit-user.component.html',
  styleUrls: ['./edit-user.component.css']
})
export class EditUserComponent implements OnInit {

  id: string;
  model: UsersListModel;
  
  constructor(private route : ActivatedRoute, 
    private toastr: ToastrService,
    private router : Router,
    private adminService: AdminService,
    private store: Store<AdminState>,
    ) { 
    }

  ngOnInit() {
    this.id = this.route.snapshot.params['id'];
    this.loadUserDetail();
  }

  onSubmit() {
    const modelFromForm = this.model;
    this.updateUserData(this.id, modelFromForm);
  }

  private loadUserDetail(){ 
    this.store.pipe(select(userSelectors.getItemById(this.id)))
    .subscribe((item : UsersListModel) => {
      this.model = item;
    });
    // this.adminService
    //     .getUserDetail(this.id)
    //     .subscribe(() => {
    //       this.store
    //         .pipe(select(state => this.model = state.users.detail))
    //         .subscribe(detail => this.model = detail);
    //     })
  }

  private updateUserData(id: string, user){
    this.adminService
        .editUser(id, user)
        .subscribe(() => {
          this.router.navigate(['/admin/panel/all-users']);
          this.toastr.success(`${this.model.username}`, 'Successfully updated');
        })
  }
}
