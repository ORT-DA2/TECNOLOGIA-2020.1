import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { IndexAdminComponent } from './index-admin/index-admin.component';
import { LogComponent } from './log/log.component';
import { ReportComponent } from './report/report.component';
import { NotFoundComponent } from '../error/not-found/not-found.component';
import { UsersComponent } from './users/users.component';


const routes: Routes = [
  {
    path:'',
    component: IndexAdminComponent,
    children:[
      {
        path:'',
        redirectTo: 'logs'
      },
      {
        path: 'logs',
        component: LogComponent
      },
      {
        path: 'users',
        component: UsersComponent 
      },
      {
        path: 'reports',
        component: ReportComponent
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
export class AdminRoutingModule { }
