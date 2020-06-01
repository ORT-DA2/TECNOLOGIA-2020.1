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
    const hw: Homework = this.service.getHomework(id);
    this.homework = hw;
  }

  onBack(): void {
    this.router.navigate(["/homeworks"]);
  }
}
