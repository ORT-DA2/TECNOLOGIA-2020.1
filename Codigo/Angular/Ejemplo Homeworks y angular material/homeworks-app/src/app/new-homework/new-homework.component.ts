import { Exercise } from "./../models/Exercise";
import { Homework } from "./../models/Homework";
import { Component, OnInit } from "@angular/core";
import { HomeworksService } from "./../services/homeworks.service";
import { Router } from "@angular/router";

@Component({
  selector: "app-new-homework",
  templateUrl: "./new-homework.component.html",
  styleUrls: ["./new-homework.component.css"],
})
export class NewHomeworkComponent implements OnInit {
  constructor(
    private serviceHomeworks: HomeworksService,
    private router: Router
  ) {}
  description = "";
  dueDate: Date;

  ngOnInit() {}

  addHomework(): void {
    const hw = new Homework(
      this.description,
      0,
      this.dueDate
      //new Array<Exercise>()
    );
    this.serviceHomeworks.postHomework(hw).subscribe(
      (data: string) => this.result(data),
      (error: any) => alert(error.message)
    );
  }
  private result(data): void {
    alert("Creado con exito...")
    this.router.navigate(["/homeworks"]);
  }
}
