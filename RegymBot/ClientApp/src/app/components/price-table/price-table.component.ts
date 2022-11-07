import { Component, OnInit, ViewChild } from "@angular/core";
import {
  MatDialog,
  MatPaginator,
  MatSnackBar,
  MatSort,
  MatTableDataSource,
} from "@angular/material";
import { Duration } from "src/app/constants/snackBarDuration";
import { ImageSnippet } from "src/app/helpers/imageSnippet";
import { PriceModel } from "src/app/models/price-model";
import { RegymClub } from "src/app/models/regym-club";
import { Services } from "src/app/models/services";
import { PriceService } from "src/app/services/price-service";
import { ModalPriceComponent } from "../modal-price/modal-price.component";

@Component({
  selector: "app-price-table",
  templateUrl: "./price-table.component.html",
  styleUrls: ["./price-table.component.scss"],
})
export class PriceTableComponent {
  RegymClub = RegymClub;
  public selectedFile: ImageSnippet;

  apolloUrl: string = '';
  vavylonUrl: string = '';
  pshknUrl: string = '';

  constructor(
    private priceService: PriceService,
    private snackBar: MatSnackBar,
  ) {
    this.initImagesSrc();
  }
 
  initImagesSrc(): void { 
    this.apolloUrl = `apollo-prices.jpg?${Date.now()}`;
    this.vavylonUrl = `vavylon-prices.jpg?${Date.now()}`;
    this.pshknUrl = `pshkn-prices.jpg?${Date.now()}`;
  }

  processFile(imageInput: any, club: RegymClub) {
    const file: File = imageInput.files[0];
    const reader = new FileReader();

    reader.addEventListener("load", (event: any) => {
      this.selectedFile = new ImageSnippet(event.target.result, file);
      this.selectedFile.pending = true;

      this.priceService
        .uploadPricesImage(this.selectedFile.file, club)
        .subscribe(
          (data) => {
            this.selectedFile.pending = false;
            this.selectedFile.status = "ok";
            this.initImagesSrc();
          },
          () => {
            this.selectedFile.pending = false;
            this.selectedFile.status = "fail";
            this.selectedFile.src = "";

            this.snackBar.open(
              "Помилка при завантаженні зображення",
              "Приховати",
              {
                duration: Duration,
              }
            );
            this.initImagesSrc();
          }
        );
    });

    reader.readAsDataURL(file);
  }
}
