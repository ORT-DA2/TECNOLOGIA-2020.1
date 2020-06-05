import { Injectable, Inject } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { SESSION_STORAGE, WebStorageService, StorageService } from 'angular-webstorage-service';
import { UserLogin } from 'src/app/models/login/user-login';
import { catchError, map, tap } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class SessionService {
  private uri = environment.WEB_API_URI+"/login";
  private static token: string = "";

  public get header(){
    return {headers: new HttpHeaders({'accept':'application/json',
                                      'Authorization': SessionService.token})};
  }
  
  constructor(private httpService: HttpClient) { }

  public Login(userLogin: UserLogin): Observable<string>{
    return this.httpService.post(this.uri, userLogin).pipe(tap((session: string) => this.finishLogin(session)), catchError(this.handleError));
  }

  private finishLogin(session: string){
    SessionService.token = session;
  }

  private handleError(error: HttpErrorResponse){
    return error.message;
  }

  public IsLoggedIn(): boolean{
    return true;
  }
}
