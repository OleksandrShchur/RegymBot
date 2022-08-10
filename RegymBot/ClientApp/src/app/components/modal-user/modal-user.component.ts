import { Component, OnInit } from "@angular/core";
import { MatDialog, MatDialogRef, MatSnackBar } from "@angular/material";
import { MatInputModule } from "@angular/material";
import { UserModel } from "src/app/models/user-model";
import { UserService } from "src/app/services/user-service";

@Component({
  selector: "app-modal-user",
  templateUrl: "./modal-user.component.html",
  styleUrls: ["./modal-user.component.css"],
})
export class ModalUserComponent implements OnInit {
  public user: UserModel;

  constructor(
    public dialogRef: MatDialogRef<ModalUserComponent>,
    private userService: UserService,
    private snackBar: MatSnackBar,
    public dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.user = {
      userGuid: "",
      name: "",
      surName: "",
      description: "",
      role: [],
    };
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  onSave(): void {
    this.userService.addUser(this.user).subscribe(
      (data: any) => {
        this.user = data;

        this.snackBar.open("Додано користувача", "Приховати", {
          duration: 10000,
        });

        this.dialog.closeAll();
      },
      (error) => {
        alert("Error");
      }
    );
  }
}
