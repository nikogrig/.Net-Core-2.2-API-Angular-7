import { Component, OnInit } from '@angular/core';
import { UsersListModel } from 'src/app/models/read.model';
import { ActivatedRoute, Router } from '@angular/router';
import { AdminService } from 'src/app/services/admin.service';
import { AdminState } from 'src/app/store/admin.state';
import { Store, select } from '@ngrx/store';
import * as userSelectors from '../../../store/selectors/users.selector';

@Component({
  selector: 'app-user-detail',
  templateUrl: './user-detail.component.html',
  styleUrls: ['./user-detail.component.css']
})
export class UserDetailComponent  implements OnInit {

  id: string;
  user: UsersListModel;

  constructor(private route : ActivatedRoute, 
    private router : Router,
    private adminService: AdminService,
    private store: Store<AdminState>
    ) { }

  ngOnInit() {
    this.id = this.route.snapshot.params['id'];
    this.store.pipe(select(userSelectors.getItemById(this.id)))
    .subscribe((item : UsersListModel) => {
      this.user = item;
    });

    // this.adminService
    //     .getUserDetail(this.id)
    //     .subscribe(() => {
    //       this.store
    //         .pipe(select(state => this.user = state.users.detail))
    //         .subscribe(detail => this.user = detail);
    //     })
  }
}