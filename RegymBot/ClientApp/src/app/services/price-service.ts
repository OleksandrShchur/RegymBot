import { Injectable } from "@angular/core";
import { BaseService } from "./base-service";
import { PriceModel } from "../models/price-model";

@Injectable()
export class PriceService extends BaseService {
  getAllPrices() {
    return this.http.get<Array<PriceModel>>(this.baseUrl + "Prices/get-all");
  }

  removePrice(guid: string) {
    return this.http.delete(`${this.baseUrl}Prices/delete-price/${guid}`);
  }

  addPrice(price: PriceModel) {
    return this.http.post(this.baseUrl + "Prices/new-price", price);
  }

  updatePrice(price: PriceModel) {
    return this.http.post(this.baseUrl + "Prices/update-price", price);
  }

  uploadPricesImage(image: File, clubIndex: number) {
    const formData = new FormData();
    formData.append("file", image, image.name);

    return this.http.post(this.baseUrl + "Prices/upload-image", formData, {
      params: {
        club: clubIndex.toString(),
      },
    });
  }
}
