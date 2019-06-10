import { Component, OnInit } from '@angular/core';
import { CreateUserModel } from 'src/app/models/write.model';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { AdminService } from 'src/app/services/admin.service';

@Component({
  selector: 'app-create-user',
  templateUrl: './create-user.component.html',
  styleUrls: ['./create-user.component.css']
})
export class CreateUserComponent implements OnInit {

  model: CreateUserModel;
  roles: Object;
  constructor(
    private toastr: ToastrService,
    private router : Router,
    private adminService: AdminService
    ) { 
      this.model = new CreateUserModel('','','','','','', '', '','', '');
    }

  ngOnInit() {
    this.adminService
    .getRoles()
    .subscribe(data => {
      this.roles = data
    });
  }

  onSubmit() {
    this.model.confirmPassword = "";
    this.adminService
    .createUser(this.model)
    .subscribe(() => {
      this.router.navigate(['/admin/panel/all-users']);
      this.toastr.success(`${this.model.username}`, 'Successfully created');
    })
  }
}
