import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { SessionService } from '../session/session.service';
import { Observable, throwError } from 'rxjs';
import { UserBasicInfo } from 'src/app/models/user/user-basic-info';
import { UserDetailInfo } from 'src/app/models/user/user-detail-info';
import { catchError, map, tap } from 'rxjs/operators';

@Injectable()
export class UserService {
  private uri = environment.WEB_API_URI + "/users";

  constructor(private httpService: HttpClient, private sessionService: SessionService) { }

  GetAll(): Observable<Array<UserBasicInfo>> {
    return this.httpService.get<UserBasicInfo[]>(this.uri, this.sessionService.header).pipe(catchError(this.handleError));
  }

  Get(userId: number): Observable<UserDetailInfo> {
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
  }

  private handleError(error: HttpErrorResponse) {
    if (error.status != 200) {
      return throwError(error);
    } else {
      return throwError(error);
    }
  };
}
