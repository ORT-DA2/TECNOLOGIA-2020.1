import { Exercise } from "./../models/Exercise";
import { Homework } from "./../models/Homework";
import { Injectable } from "@angular/core";

@Injectable({
  providedIn: "root",
})
export class HomeworksService {
  constructor() {}
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
}
