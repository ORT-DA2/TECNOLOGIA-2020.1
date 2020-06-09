import { Exercise } from "./../models/Exercise";
import { Homework } from "./../models/Homework";
import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs";
import { map, tap, catchError } from 'rxjs/operators';

@Injectable({
  providedIn: "root",
})
export class HomeworksService {
  private WEB_API_URL: string = "https://localhost:5001/";
  constructor(private _httpService: HttpClient) {}
  /*
  homeworks = [
    new Homework(1, "Hacer obligatorio 1", 3, new Date(), [
      new Exercise(1, "Desarrollar back-end", 1),
      new Exercise(2, "Escribir documentacion", 10),
    ]),
    new Homework(2, "Hacer obligatorio 2", 1, new Date(), [
      new Exercise(1, "Modificar back-end", 1),
      new Exercise(1, "Modificar front-end", 4),
      new Exercise(2, "Escribir documentacion", 10),
    ]),
  ];

  getHomeworks(): Array<Homework> {
    return this.homeworks;
  }

  getHomework(id: number): Homework {
    return this.homeworks.find((x) => x.id === id);
  }

  postHomework(hw: Homework) {
    let id = this.homeworks[this.homeworks.length - 1].id;
    hw.id = id + 1;
    this.homeworks.push(hw);
  }
*/
  getHomeworks(): Observable<Array<Homework>> {
    const myHeaders = new HttpHeaders();
    myHeaders.append("Accept", "application/json");
    const httpOptions = {
      headers: myHeaders,
    };

    return this._httpService.get<Array<Homework>>(
      this.WEB_API_URL + "Homeworks",
      httpOptions
    );
  }
  getHomework(id: number): Observable<Homework> {
    const myHeaders = new HttpHeaders();
    myHeaders.append("Accept", "application/json");
    const httpOptions = {
      headers: myHeaders,
    };

    return this._httpService.get<Homework>(
      this.WEB_API_URL + "Homeworks/" + id,
      httpOptions
    );
  }

  postHomework(hw: Homework): Observable<string> {
    const myHeaders = new HttpHeaders();
    myHeaders.append("Accept", "application/text");
    const httpOptions = {
      headers: myHeaders,
    };
    return this._httpService.post<string>(
      this.WEB_API_URL + "Homeworks",
      hw,
      httpOptions
    );
  }
}
