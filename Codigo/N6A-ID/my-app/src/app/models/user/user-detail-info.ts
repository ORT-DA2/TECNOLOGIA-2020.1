
export class UserDetailInfo {
    public id: number;
    public name: string;
    public lastname: string;
    public username: string;
    public email: string;
    public password: string;

    constructor(user?: UserDetailInfo){
        if(user != null){
            this.id = user.id;
            this.name = user.name;
            this.lastname = user.lastname;
            this.username = user.username;
            this.email = user.email;
            this.password = user.password;
        }
    }
}
