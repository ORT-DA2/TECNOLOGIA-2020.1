import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { NotFoundComponent } from '../error/not-found/not-found.component';
import { UsersComponent } from './users/users.component';
import { TeamsComponent } from './teams/teams.component';


const routes: Routes = [
  {
    path:'',
    component: HomeComponent,
    children:[
      {
        path:'',
        redirectTo: 'users'
      },
      {
        path: 'users',
        component: UsersComponent
      },
      {
        path: 'teams',
        component: TeamsComponent
      }
    ]
  },
  {
    path: '**',
    component: NotFoundComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HomeRoutingModule { }
