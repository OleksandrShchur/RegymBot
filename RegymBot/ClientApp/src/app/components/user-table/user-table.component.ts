import { Component, ViewChild } from "@angular/core";
import {
  MatPaginator,
  MatSnackBar,
  MatTableDataSource,
} from "@angular/material";
import { MatDialog } from "@angular/material/dialog";
import { UserModel } from "src/app/models/user-model";
import { UserService } from "src/app/services/user-service";
import { ModalUserComponent } from "../modal-user/modal-user.component";

@Component({
  selector: "app-user-table",
  templateUrl: "./user-table.component.html",
  styleUrls: ["./user-table.component.css"],
})
export class UserTableComponent {
  private userList: Array<UserModel> | any;
  private guidColumn: string = "userGuid";

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
    private snackBar: MatSnackBar,
    public dialog: MatDialog
  ) {}

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;

  ngOnInit() {
    this.userService.getAllUsers().subscribe(
      (data: Array<UserModel>) => {
        this.userList = data;
        this.dataSource = new MatTableDataSource(this.userList);

        this.dataSource.paginator = this.paginator;
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

  deleteUser(guid: string) {
    this.userService.removeUser(guid).subscribe(
      () => {
        alert("User deleted");
        const itemIndex = this.dataSource.data.findIndex(
          (obj) => obj[this.guidColumn] == guid
        );
        this.dataSource.data.slice(itemIndex, 1);
        this.dataSource.paginator = this.paginator;
      },
      (error) => {
        alert("Failed to delete user");
      }
    );
  }

  openDialog(): void {
    this.dialog.open(ModalUserComponent);
  }
}
