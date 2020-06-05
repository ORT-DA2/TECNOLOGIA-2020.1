import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/services/user/user.service';
import { UserBasicInfo } from 'src/app/models/user/user-basic-info';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {
  users: UserBasicInfo[] = [];

  constructor(private userService: UserService) { }

  ngOnInit() {
    this.userService.GetAll().subscribe((usersResponse):UserBasicInfo[] => this.users=usersResponse, (error: any) => this.showMessage(JSON.stringify(error)));
  }

  private showMessage(message: string){
    alert(message);
  }
}
