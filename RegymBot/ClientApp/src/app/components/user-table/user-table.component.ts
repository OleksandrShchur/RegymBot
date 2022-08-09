import { AfterViewInit, Component } from "@angular/core";
import { MatTableDataSource } from "@angular/material";
import { UserModel } from "src/app/models/user-model";
import { UserService } from "src/app/services/user-service";

@Component({
  selector: "app-user-table",
  templateUrl: "./user-table.component.html",
  styleUrls: ["./user-table.component.css"],
})
export class UserTableComponent implements AfterViewInit {
  private userList: Array<UserModel> | any;
  public dataSource;

  constructor(private userService: UserService) {}

  ngAfterViewInit() {
    this.userList = this.userService.getAllUsers();
    this.dataSource = new MatTableDataSource(this.userList);
  }
}
