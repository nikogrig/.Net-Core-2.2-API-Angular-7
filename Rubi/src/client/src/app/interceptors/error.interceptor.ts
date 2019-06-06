import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpErrorResponse } from '@angular/common/http'
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
	
	constructor(private router: Router, private toastr: ToastrService){}

	intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
			return next.handle(request)
			.pipe(catchError((err: HttpErrorResponse) => {
                switch(err.status){
                    case 400:
                       this.toastr.error(this.errorMessageResult(err))
                       this.router.navigate(['/register']);
                      break;
                    case 401:
                        this.toastr.error(this.errorMessageResult(err))
                    this.router.navigate(['/error/401']);
                      break;
                    case 403:
                        this.toastr.error(this.errorMessageResult(err))                    
                        this.router.navigate(['/error/403']);
                      break; //forbiden
                    case 0:
                        this.toastr.error(this.errorMessageResult(err))                  
                    this.router.navigate(['/error/500']);
                      break;
                    // default:
                    // this.toastr.error(err.statusText, 'Error!')
                    //   break;
                }
                return throwError(err);
            })); 
  }

    private errorMessageResult (err : HttpErrorResponse) : string {
    let er: string = '';
    for(let el in err.error){
      er += err.error[el] + '\n';
    };
    return er;
  };
}