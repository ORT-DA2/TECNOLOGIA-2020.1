import { Component, OnInit, Input } from '@angular/core';
import { RouteInfoSideBar } from '../../../models/navigation/routeInfoSideBar';

@Component({
  selector: 'app-side-bar',
  templateUrl: './side-bar.component.html',
  styleUrls: ['./side-bar.component.css']
})
export class SideBarComponent implements OnInit {
  @Input() menuItems: Array<RouteInfoSideBar>;
  constructor() { }

  ngOnInit() {
  }

}
