import { HttpResponse, HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http'
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ProfileService } from '../services/profile.service';

@Injectable()
export class ProfileInterceptor implements HttpInterceptor {
	
	constructor(private profileService: ProfileService
		) { }

	intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
		return next.handle(request)
			.pipe(tap((response: HttpResponse<any>) => {				
				// if (response instanceof HttpResponse && response.url.includes('profile')){ //
				// 	 this.getProfile(response.body);
				//  }					 
		}));
	}

    private getProfile(response) {
        return response;
    }
	
}