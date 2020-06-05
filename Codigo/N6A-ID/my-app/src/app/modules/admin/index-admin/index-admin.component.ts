import { Component, OnInit } from '@angular/core';
import { RouteInfoSideBar } from 'src/app/models/navigation/routeInfoSideBar';

@Component({
  selector: 'app-index-admin',
  templateUrl: './index-admin.component.html',
  styleUrls: ['./index-admin.component.css']
})
export class IndexAdminComponent implements OnInit {
  adminItems: Array<RouteInfoSideBar> = [
    { path: 'dashboard', title: 'Dashboard', icon: 'dashboard', class: '' },
    { path: 'logs', title: 'Logs', icon: 'dashboard', class: '' },
    { path: 'users', title: 'Users', icon: 'accessibility_new', class: '' },
    { path: 'areas', title: 'Areas', icon: 'business', class: '' }]

  constructor() { }

  ngOnInit() {
  }

}
