import { UserDetailInfo } from "./user-detail-info";

export class UserBasicInfo {
    public id: number;
    public name: string;

    constructor(user?: UserBasicInfo) {
        if (user != null) {
            this.id = user.id;
            this.name = user.name;
        }
    }
}
