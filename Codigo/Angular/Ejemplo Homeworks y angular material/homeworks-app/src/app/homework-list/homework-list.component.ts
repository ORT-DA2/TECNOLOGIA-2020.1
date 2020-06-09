import { HomeworksService } from './../services/homeworks.service';
import { Component, OnInit } from '@angular/core';
import { Homework } from '../models/Homework';
import { Exercise } from '../models/Exercise';

@Component({
  selector: 'app-homework-list',
  templateUrl: './homework-list.component.html',
  styleUrls: ['./homework-list.component.css'],
})
export class HomeworkListComponent implements OnInit {
  displayedColumns: string[] = ['id', 'description', 'duedate', 'score'];
  dataSource = [];
  listFilter = '';
  onoff = false;
  pageTitle: string;

  constructor(private serviceHomeworks: HomeworksService) {}

  ngOnInit() {
    this.serviceHomeworks.getHomeworks().subscribe(
      ((data: Array<Homework>) => this.result(data)),
      ((error: any) => alert(error.message))
    );
  }
  private result(data: Array<Homework>): void {
    this.dataSource = data;
  }
  onoffChange(): void {
    this.onoff = !this.onoff;
  }

  onRatingClicked(message: string): void {
    this.pageTitle = message;
  }
}
