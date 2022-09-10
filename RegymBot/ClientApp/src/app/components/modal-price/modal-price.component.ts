import { Component, OnInit, Input } from "@angular/core";
import { FormControl, FormGroup } from "@angular/forms";
import { MatDialog, MatDialogRef, MatSnackBar } from "@angular/material";
import { MatInputModule } from "@angular/material";
import { Duration } from "src/app/constants/snackBarDuration";
import { PriceModel } from "src/app/models/price-model";
import { Services } from "src/app/models/services";
import { PriceService } from "src/app/services/price-service";

@Component({
  selector: "app-modal-price",
  templateUrl: "./modal-price.component.html",
  styleUrls: ["./modal-price.component.css"],
})
export class ModalPriceComponent implements OnInit {
  Services = Services;
  @Input() public price: PriceModel;
  public priceForm: FormGroup;

  constructor(
    public dialogRef: MatDialogRef<ModalPriceComponent>,
    private priceService: PriceService,
    private snackBar: MatSnackBar,
    public dialog: MatDialog
  ) {}

  ngOnInit(): void {
    if (this.price === null) {
      this.priceForm = new FormGroup({
        priceType: new FormControl(""),
        priceName: new FormControl(""),
        price: new FormControl(Services.Training),
      });
    } else {
      this.priceForm = new FormGroup({
        priceType: new FormControl(this.price.priceType),
        priceName: new FormControl(this.price.priceName),
        price: new FormControl(this.price.price),
      });
    }
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  onSubmit(): void {
    if (this.price === null) {
      let newPrice = new PriceModel();

      newPrice.priceType = this.priceForm.value.priceType;
      newPrice.priceName = this.priceForm.value.priceName;
      newPrice.price = this.priceForm.value.price;

      this.priceService.addPrice(newPrice).subscribe(
        () => {
          this.snackBar.open("Ціну/послугу додано", "Приховати", {
            duration: Duration,
          });

          this.dialog.closeAll();
        },
        () => {
          this.snackBar.open(
            "Помилка при додаванні ціни/послуги",
            "Приховати",
            {
              duration: Duration,
            }
          );
        }
      );
    } else {
      this.price.priceType = this.priceForm.value.priceType;
      this.price.priceName = this.priceForm.value.priceName;
      this.price.price = this.priceForm.value.price;

      this.priceService.updatePrice(this.price).subscribe(
        () => {
          this.snackBar.open("Дані про ціну/послугу оновлено", "Приховати", {
            duration: Duration,
          });

          this.dialog.closeAll();
        },
        () => {
          this.snackBar.open(
            "Не вдалося оновити дані про ціну/послугу",
            "Приховати",
            {
              duration: Duration,
            }
          );
        }
      );
    }
  }
}
