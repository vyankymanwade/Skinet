import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse,
  HttpStatusCode
} from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private router:Router,private toaster:ToastrService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError((error:HttpErrorResponse) =>{
        if(error){

          if(error.status === HttpStatusCode.BadRequest){
            if(error.error.errors){
              throw error.error;
            }else{
              this.toaster.error(error.error.message,error.status.toString());
            }
          }

          if(error.status === HttpStatusCode.Unauthorized){
            this.toaster.error(error.error.message,error.status.toString());
          }

          if(error.status === HttpStatusCode.NotFound){
            this.router.navigateByUrl('/not-found');
          }

          if(error.status == HttpStatusCode.InternalServerError){
            const navigationExtras:NavigationExtras = {state:{error:error.error}};
            this.router.navigateByUrl('/server-error',navigationExtras);
          }
        }

        return throwError(() => new Error(error.message));
      })
    );
  }
}
