import { Injectable } from "@angular/core";
import { AdminsInfo } from "../models/admins-info";
import { AdminsRegistrationLinks } from "../models/admins-registration-links";
import { BaseService } from "./base-service";

@Injectable()
export class AdminsInfoService extends BaseService {
  get() {
    return this.http.get<AdminsInfo>(
      this.baseUrl + "AdminsInfo"
    );
  }

  getAdminsRegistrationLinks() {
    return this.http.get<AdminsRegistrationLinks>(
      this.baseUrl + "AdminsInfo/registration-links"
    );
  }

  update(adminsInfo: AdminsInfo) {
    return this.http.post(this.baseUrl + "AdminsInfo", adminsInfo);
  }
}
