import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HomeRoutingModule } from './home-routing.module';
import { HomeComponent } from './home/home.component';
import { ErrorModule } from '../error/error.module';
import { UsersComponent } from './users/users.component';
import { TeamsComponent } from './teams/teams.component';


@NgModule({
  declarations: [HomeComponent, UsersComponent, TeamsComponent],
  imports: [
    CommonModule,
    HomeRoutingModule,

    ErrorModule
  ]
})
export class HomeModule { }
