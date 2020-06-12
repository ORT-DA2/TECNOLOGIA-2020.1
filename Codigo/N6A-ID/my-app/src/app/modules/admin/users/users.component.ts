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
  response:any = {keys: "", body:""};

  constructor(private userService: UserService) { }

  ngOnInit() {
    //this.userService.GetAll().subscribe((userBasicInfo: UserBasicInfo[]) => this.users = userBasicInfo, messageError => this.response.body = messageError);
    this.userService.GetAll().subscribe((users: UserBasicInfo[]) => this.users = users, messageError => this.response.body = messageError);
    
    /*this.userService.GetAllResponse().subscribe(resp => {
      const keys = resp.headers.keys();
      this.response.keys = keys.map(key => `${key}: ${resp.headers.get(key)}`);
      this.response.body = resp.body; 
    })*/
  }

  private showMessage(message: string) {
    alert(message);
  }
}
