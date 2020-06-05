import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AdminRoutingModule } from './admin-routing.module';
import { IndexAdminComponent } from './index-admin/index-admin.component';
import { LogComponent } from './log/log.component';
import { ReportComponent } from './report/report.component';
import { ErrorModule } from '../error/error.module';
import { NavigationModule } from '../navigation/navigation.module';
import { RouterModule } from '@angular/router';
import { UsersComponent } from './users/users.component';
import { UserService } from 'src/app/services/user/user.service';


@NgModule({
  declarations: [IndexAdminComponent, LogComponent, ReportComponent, UsersComponent],
  imports: [
    CommonModule,
    AdminRoutingModule,

    ErrorModule,
    NavigationModule
  ],
  providers:[UserService]
})
export class AdminModule { }
