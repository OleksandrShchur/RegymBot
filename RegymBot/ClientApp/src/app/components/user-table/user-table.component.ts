import { Component, ViewChild } from "@angular/core";
import {
  MatPaginator,
  MatSnackBar,
  MatSort,
  MatTableDataSource,
} from "@angular/material";
import { MatDialog } from "@angular/material/dialog";
import { Duration } from "src/app/constants/snackBarDuration";
import { UserModel } from "src/app/models/user-model";
import { UserService } from "src/app/services/user-service";
import { ModalUserComponent } from "../modal-user/modal-user.component";

@Component({
  selector: "app-user-table",
  templateUrl: "./user-table.component.html",
  styleUrls: ["./user-table.component.css"],
})
export class UserTableComponent {
  private userList: Array<UserModel>;
  private guidColumn: string = "userGuid";

  public displayedColumns: string[] = [
    "avatar",
    "name",
    "surName",
    "description",
    "roles",
    "actions",
  ];
  public dataSource: MatTableDataSource<UserModel>;

  constructor(
    private userService: UserService,
    private snackBar: MatSnackBar,
    public dialog: MatDialog
  ) {}
  @ViewChild(MatSort, {static: true}) sort: MatSort;
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;

  ngOnInit() {
    this.loadUsers();
  }

  loadUsers() { 
    this.userService.getAllUsers().subscribe(
      (data: Array<UserModel>) => {
        this.userList = data;
        this.dataSource = new MatTableDataSource(this.userList);

        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      },
      () => {
        this.snackBar.open(
          "Помилка при завантаженні списку користувачів.",
          "Приховати",
          {
            duration: Duration,
          }
        );
      }
    );
  }

  deleteUser(guid: string) {
    this.userService.removeUser(guid).subscribe(
      () => {
        this.snackBar.open("Користувача видалено", "Приховати", {
          duration: Duration,
        });

        this.loadUsers();
      },
      () => {
        this.snackBar.open("Помилка при видаленні користувача", "Приховати", {
          duration: Duration,
        });
      }
    );
  }

  editUser(user: UserModel) {
    const dialogRef = this.dialog.open(ModalUserComponent);

    dialogRef.componentInstance.user = user;
    dialogRef.afterClosed().subscribe(() => { 
      this.loadUsers();
    });
  }

  addUser(): void {
    const dialogRef = this.dialog.open(ModalUserComponent);

    dialogRef.componentInstance.user = null;
    dialogRef.afterClosed().subscribe(() => { 
      this.loadUsers();
    });
  }
}
