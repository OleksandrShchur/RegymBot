import { Component, OnInit, ViewChild } from "@angular/core";
import {
  MatDialog,
  MatPaginator,
  MatSnackBar,
  MatTableDataSource,
} from "@angular/material";
import { Duration } from "src/app/constants/snackBarDuration";
import { PriceModel } from "src/app/models/price-model";
import { PriceService } from "src/app/services/price-service";
import { ModalPriceComponent } from "../modal-price/modal-price.component";

@Component({
  selector: "app-price-table",
  templateUrl: "./price-table.component.html",
  styleUrls: ["./price-table.component.css"],
})
export class PriceTableComponent implements OnInit {
  private priceList: Array<PriceModel>;
  private guidColumn: string = "priceGuid";

  public displayedColumns: string[] = [
    "priceType",
    "priceName",
    "price",
    "actions",
  ];
  public dataSource;

  constructor(
    private priceService: PriceService,
    private snackBar: MatSnackBar,
    public dialog: MatDialog
  ) {}

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;

  ngOnInit() {
    this.priceService.getAllPrices().subscribe(
      (data: Array<PriceModel>) => {
        this.priceList = data;
        this.dataSource = new MatTableDataSource(this.priceList);

        this.dataSource.paginator = this.paginator;
      },
      () => {
        this.snackBar.open(
          "Помилка при завантаженні списку цін.",
          "Приховати",
          {
            duration: Duration,
          }
        );
      }
    );
  }

  deletePrice(guid: string) {
    this.priceService.removePrice(guid).subscribe(
      () => {
        this.snackBar.open("Ціну/послугу видалено", "Приховати", {
          duration: Duration,
        });

        const itemIndex = this.dataSource.data.findIndex(
          (obj) => obj[this.guidColumn] == guid
        );
        this.dataSource.data.slice(itemIndex, 1);
        this.dataSource.paginator = this.paginator;
      },
      () => {
        this.snackBar.open("Помилка при видаленні ціни/послуги", "Приховати", {
          duration: Duration,
        });
      }
    );
  }

  editPrice(price: PriceModel) {
    const dialogRef = this.dialog.open(ModalPriceComponent);

    dialogRef.componentInstance.price = price;
  }

  addPrice(): void {
    const dialogRef = this.dialog.open(ModalPriceComponent);

    dialogRef.componentInstance.price = null;
  }
}
