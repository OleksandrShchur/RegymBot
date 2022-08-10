import { Injectable } from "@angular/core";
import { UserModel } from "../models/user-model";
import { BaseService } from "./base-service";

@Injectable()
export class UserService extends BaseService {
  private;
  getAllUsers() {
    return this.http.get<Array<UserModel>>(this.baseUrl + "Users/get-all");
  }

  removeUser(guid: string) {
    return this.http.delete(`${this.baseUrl}Users/delete-user/${guid}`);
  }

  addUser(user: UserModel) {
    return this.http.post<UserModel>(this.baseUrl + "Users/new-user", user);
  }
}