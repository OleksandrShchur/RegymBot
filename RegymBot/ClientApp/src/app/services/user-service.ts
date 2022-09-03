import { Injectable } from "@angular/core";
import { UserModel } from "../models/user-model";
import { BaseService } from "./base-service";

@Injectable()
export class UserService extends BaseService {
  getAllUsers() {
    return this.http.get<Array<UserModel>>(this.baseUrl + "Users/get-all");
  }

  removeUser(guid: string) {
    return this.http.delete(`${this.baseUrl}Users/delete-user/${guid}`);
  }

  addUser(user: UserModel, image: File) {
    const formData = this.createFormData(image);

    //return this.http.post(this.baseUrl + "Users/new-user", user);
    return this.http.post(this.baseUrl + "Users/new-user", {
      formData,
      user,
    });
  }

  updateUser(user: UserModel, image: File) {
    const formData = this.createFormData(image);

    //return this.http.post(this.baseUrl + "Users/update-user", user);
    return this.http.post(this.baseUrl + "Users/update-user", {
      formData,
      user,
    });
  }

  uploadUserAvatar(image: File, guid: string) {
    const formData = this.createFormData(image);

    return this.http.post(this.baseUrl + "Users/upload-avatar", formData, {
      params: {
        userGuid: guid,
      },
    });
  }

  private createFormData(image: File): FormData {
    const formData = new FormData();
    formData.append("file", image, image.name);

    return formData;
  }
}
