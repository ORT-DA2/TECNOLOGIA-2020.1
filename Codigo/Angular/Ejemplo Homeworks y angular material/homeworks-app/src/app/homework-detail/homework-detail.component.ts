import { Component, OnInit } from "@angular/core";
import { Homework } from "../models/Homework";
import { ActivatedRoute, Router } from "@angular/router";
import { HomeworksService } from "../services/homeworks.service";

@Component({
  selector: "app-homework-detail",
  templateUrl: "./homework-detail.component.html",
  styleUrls: ["./homework-detail.component.css"],
})
export class HomeworkDetailComponent implements OnInit {
  homework: Homework;

  constructor(
    private currentRoute: ActivatedRoute,
    private router: Router,
    private service: HomeworksService
  ) {}

  ngOnInit() {
    let id = + this.currentRoute.snapshot.params["id"];
    this.service.getHomework(id).subscribe(
      ((data: Homework) => this.result(data)),
      ((error: any) => alert(error.message))
    );
  }

  private result(data: Homework): void {
    this.homework = data;
  }

  onBack(): void {
    this.router.navigate(["/homeworks"]);
  }
}
