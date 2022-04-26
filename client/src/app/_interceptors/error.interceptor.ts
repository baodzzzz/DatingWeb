import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { Router } from '@angular/router';
import { Toast, ToastrService } from 'ngx-toastr';
import { error } from 'console';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private router: Router, private toastr: ToastrService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
       catchError(error =>{
         if(error) {
           switch(error.status){
            case 400:
              if(error.error.errors)
              break;
            default:
              break;
           }
         }
         return throwError(error);
       })
    )
  }
}
