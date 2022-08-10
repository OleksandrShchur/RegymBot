import { Component, ViewChild } from "@angular/core";
import {
  MatPaginator,
  MatSnackBar,
  MatTableDataSource,
} from "@angular/material";
import { UserModel } from "src/app/models/user-model";
import { UserService } from "src/app/services/user-service";

@Component({
  selector: "app-user-table",
  templateUrl: "./user-table.component.html",
  styleUrls: ["./user-table.component.css"],
})
export class UserTableComponent {
  private userList: Array<UserModel> | any;

  public displayedColumns: string[] = [
    "name",
    "surName",
    "description",
    "roles",
    "actions",
  ];
  public dataSource;

  constructor(
    private userService: UserService,
    private snackBar: MatSnackBar
  ) {}

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;

  ngOnInit() {
    this.userService.getAllUsers().subscribe(
      (data: Array<UserModel>) => {
        this.userList = data;
        this.dataSource = new MatTableDataSource(this.userList);
      },
      (error) => {
        alert("Помилка");
        // this.snackBar.open(
        //   "Помилка при завантаженні списку користувачів. " + error.message,
        //   "Приховати",
        //   {
        //     duration: 10000,
        //   }
        // );
      }
    );
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }
}
