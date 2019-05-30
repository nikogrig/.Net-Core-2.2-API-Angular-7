import { HttpResponse, HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http'
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {
	
	constructor(private router: Router, 
		private authService: AuthService, 
		private toastr: ToastrService
		){ }

	intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
		if (this.authService.authtoken) {
			request = request.clone({
				setHeaders: {
                    Authorization : `Bearer ${this.authService.authtoken}`,
                    'Content-Type': 'application/json',
			   }
			});
		}	

		return next.handle(request)
			.pipe(tap((response: HttpResponse<any>) => { //TODO: /register
				if (response instanceof HttpResponse && (response.url.endsWith('login') || response.url.endsWith('register'))) {	//&& response.body.result.token	
					this.saveDataStorage(response.body.result);
				}
				
				if (response instanceof HttpResponse && response.url.endsWith('login')) {
                    this.toastr.success(`Hello ${response.body.result.username}`, 'You have successfully login!')				
				}

				if (response instanceof HttpResponse && response.url.endsWith('register')) {
                    this.toastr.success(`$Your new username is ${response.body.result.username}`, 'You have successfully register!')
				}
		}));
	}

    private saveDataStorage(data) {
		this.authService.authentificate(data);
        this.router.navigate(['']);
	}
}