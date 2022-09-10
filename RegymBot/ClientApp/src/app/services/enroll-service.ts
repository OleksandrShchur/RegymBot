import { Injectable } from "@angular/core";
import { EnrollModel } from "../models/enroll-model";
import { BaseService } from "./base-service";

@Injectable()
export class EnrollService extends BaseService {
  getAll() {
    return this.http.get<Array<EnrollModel>>(
      this.baseUrl + "Enrolls"
    );
  }

  updateMessage(enroll: EnrollModel) {
    return this.http.post(this.baseUrl + "Enrolls", enroll);
  }
}
