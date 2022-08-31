import { Injectable } from "@angular/core";
import { BaseService } from "./base-service";

@Injectable()
export class ScheduleService extends BaseService {
  uploadScheduleImage(image: File, clubIndex: number) {
    const formData = new FormData();
    formData.append("file", image, image.name);

    return this.http.post(this.baseUrl + "Schedules/upload-image", formData, {
      params: {
        club: clubIndex.toString(),
      },
    });
  }
}
