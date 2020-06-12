import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders, HttpResponse } from '@angular/common/http';
import { SessionService } from '../session/session.service';
import { Observable, throwError } from 'rxjs';
import { UserBasicInfo } from 'src/app/models/user/user-basic-info';
import { UserDetailInfo } from 'src/app/models/user/user-detail-info';
import { catchError, map, tap } from 'rxjs/operators';
import { environment } from 'src/environments/environment';

@Injectable()
export class UserService {
  private uri = environment.WEB_API_URI + "/users";

  constructor(private httpService: HttpClient) { }

  GetAll(): Observable<UserBasicInfo[]>{
    return this.httpService.get<UserBasicInfo[]>(this.uri).pipe(catchError(this.handleError));
  }
  /*
    UserComponent -suscribe> UserService -request> WEB-API
    1s
    WEB-API -response> UserService -notifica a los que estan suscriptos> UserComponent -muestra datos en UI-
  */

  GetAllTwo(): Observable<UserBasicInfo[]>{
    return this.httpService.get<UserBasicInfo[]>(this.uri);
  }

  GetAllResponse(): Observable<HttpResponse<UserBasicInfo[]>>{
    return this.httpService.get<UserBasicInfo[]>(this.uri, {observe: 'response'});
  }

  /*Get(userId: number): Observable<UserDetailInfo> {
    return this.httpService.get<UserDetailInfo>(this.uri + "/" + userId, this.sessionService.header).pipe(catchError(this.handleError));
  }

  Add(user: UserDetailInfo): Observable<UserDetailInfo> {
    return this.httpService.post<UserDetailInfo>(this.uri, user, this.sessionService.header).pipe(catchError(this.handleError));
  }

  Edit(userId: number, user: UserDetailInfo): Observable<{}> {
    return this.httpService.put(this.uri + "/" + userId, user, this.sessionService.header).pipe(catchError(this.handleError));
  }

  Delete(userId: number): Observable<{}> {
    return this.httpService.delete(this.uri + "/" + userId, this.sessionService.header).pipe(catchError(this.handleError));
  }*/

  private handleError(error: HttpErrorResponse) {
    let message: string;

    if(error.error instanceof ErrorEvent){
      //Error de conexion del lado del cliente

      message = "Error: do it again";
    }else{
      //El backend respondio con status code de error
      //el body de la response debe de dar mas informacion

      if(error.status == 0){
        message = "The server is shutdown";
      }else{
        //Depende de como me mande la api la response del error es lo que tengo que agarrar
        message = error.error.message;
      }
    }

    //Retornamos un Observable de tipo error para el que usa el servicio
    return throwError(message);
  };
}
