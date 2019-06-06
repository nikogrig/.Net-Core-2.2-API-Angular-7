import { HttpResponse, HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http'
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AdminService } from '../services/admin.service';

@Injectable()
export class AdminInterceptor implements HttpInterceptor {
	
	constructor(private adminService: AdminService
		) { }

	intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
		return next.handle(request)
			.pipe(tap((response: HttpResponse<any>) => {				
				if (response instanceof HttpResponse && response.url.endsWith('users')){ //
					 this.getListUsers(response.body);
				 }

				// with selector from store 
				// if (response instanceof HttpResponse && response.url.includes('user-detail')){ //
				//  	this.getUserDetail(response.body);
				// }
				 
				//  if (response instanceof HttpResponse && response.url.includes('edit-user')){ //
				// 	this.editUserDetail(response.body); // TODO: show updated response in pop-up modal component
				//  }			 
		}));
	}

	private getListUsers(response){
		this.adminService.pushUsers(response);
	}
	
	// with selector from store
	// private getUserDetail(response){ 
	// 	this.adminService.takeUserDetail(response);
	// }

	// private editUserDetail(response){
	// 	this.adminService.getEditedUserData(response);
	// }
}