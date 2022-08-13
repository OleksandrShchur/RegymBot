import { Injectable } from "@angular/core";
import { MessageModel } from "../models/message-model";
import { BaseService } from "./base-service";

@Injectable()
export class MessageService extends BaseService {
  getAllMessages() {
    return this.http.get<Array<MessageModel>>(
      this.baseUrl + "Messages/get-all"
    );
  }

  updateMessage(message: MessageModel) {
    return this.http.post(this.baseUrl + "Messages/update-message", message);
  }
}
