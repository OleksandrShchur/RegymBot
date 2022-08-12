import { Component, OnInit, ViewChild } from "@angular/core";
import { MatDialog, MatPaginator, MatSnackBar } from "@angular/material";
import { PriceModel } from "src/app/models/price-model";

@Component({
  selector: "app-price-service-module",
  templateUrl: "./price-service-module.component.html",
  styleUrls: ["./price-service-module.component.css"],
})
export class PriceServiceModuleComponent implements OnInit {
  private priceList: Array<PriceModel>;
  private guidColumn: string = "priceGuid";

  public displayedColumns: string[] = ["type", "name", "price"];
  public dataSource;

  constructor(private snackBar: MatSnackBar, public dialog: MatDialog) {}

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;

  ngOnInit() {}
}
