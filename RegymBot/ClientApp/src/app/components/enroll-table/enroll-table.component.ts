import { Component, OnInit, ViewChild } from "@angular/core";
import {
  MatPaginator,
  MatSnackBar,
  MatSort,
  MatTableDataSource,
} from "@angular/material";
import { Duration } from "src/app/constants/snackBarDuration";
import { EnrollModel } from "src/app/models/enroll-model";
import { RegymClub } from "src/app/models/regym-club";
import { EnrollService } from "src/app/services/enroll-service";

@Component({
  selector: "app-enroll-table",
  templateUrl: "./enroll-table.component.html",
  styleUrls: ["./enroll-table.component.scss"],
})
export class EnrollTableComponent implements OnInit {
  private enrollList: Array<EnrollModel>;
  RegymClub = RegymClub;
  public selectedClub = RegymClub.None;
  public displayedColumns: string[] = ["name", "enrol", "selectedClub", "phone", "proceed", "dateCreated"];
  @ViewChild(MatSort, {static: true}) sort: MatSort;
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  public dataSource: MatTableDataSource<EnrollModel>;

  constructor(
    private snackBar: MatSnackBar,
    private enrollService: EnrollService,
  ) {}

  ngOnInit() {
    this.dataSource = new MatTableDataSource(this.enrollList);
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
    this.dataSource.filterPredicate = (data: EnrollModel, filter: string) => {
      return filter === RegymClub.None.toString() || data.selectedClub.toString() === filter;
     };

    this.enrollService.getAll().subscribe(
      (data) => {
        this.enrollList = data;
        this.dataSource.data = this.enrollList;
      },
      () => {
        this.snackBar.open(
          "Помилка при завантаженні списку записів.",
          "Приховати",
          {
            duration: Duration,
          }
        );
      }
    );
  }

  filter(selectedClub): void { 
    this.dataSource.filter = selectedClub.toString();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  onUpdate(model: EnrollModel): void {
    this.enrollService.updateMessage(model).subscribe(
      () => {
        this.snackBar.open("Запис оновлено", "Приховати", {
          duration: Duration,
        });
      },
      () => {
        this.snackBar.open("Не вдалося оновити Запис", "Приховати", {
          duration: Duration,
        });
      }
    );
  }
}
