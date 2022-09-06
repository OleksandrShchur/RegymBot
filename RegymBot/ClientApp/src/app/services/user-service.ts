import { Injectable } from "@angular/core";
import { UserModel } from "../models/user-model";
import { BaseService } from "./base-service";
import { v4 as uuid } from "uuid";

@Injectable()
export class UserService extends BaseService {
  getAllUsers() {
    return this.http.get<Array<UserModel>>(this.baseUrl + "Users/get-all");
  }

  removeUser(guid: string) {
    return this.http.delete(`${this.baseUrl}Users/delete-user/${guid}`);
  }

  addUser(user: UserModel, image: File) {
    user.userGuid = uuid.v4();

    this.http.post(this.baseUrl + "Users/new-user", user);

    return this.uploadUserAvatar(image, user.userGuid);
  }

  updateUser(user: UserModel, image: File) {
    this.http.post(this.baseUrl + "Users/update-user", user);

    return this.uploadUserAvatar(image, user.userGuid);
  }

  uploadUserAvatar(image: File, guid: string) {
    const formData = new FormData();
    formData.append("file", image, image.name);

    return this.http.post(this.baseUrl + "Users/upload-avatar", formData, {
      params: {
        userGuid: guid,
      },
    });
  }
}
