import { Injectable } from "@angular/core";
import { FeedbackModel } from "../models/feedback-model";
import { BaseService } from "./base-service";

@Injectable()
export class FeedbackService extends BaseService {
  getAllFeedbacks() {
    return this.http.get<Array<FeedbackModel>>(
      this.baseUrl + "Feedbacks/get-all"
    );
  }
}
