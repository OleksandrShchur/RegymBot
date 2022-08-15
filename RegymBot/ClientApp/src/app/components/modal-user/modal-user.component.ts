import { Component, Input, OnInit } from "@angular/core";
import { FormControl, FormGroup } from "@angular/forms";
import { MatDialog, MatDialogRef, MatSnackBar } from "@angular/material";
import { MatInputModule } from "@angular/material";
import { Duration } from "src/app/constants/snackBarDuration";
import { UserModel } from "src/app/models/user-model";
import { UserService } from "src/app/services/user-service";

@Component({
  selector: "app-modal-user",
  templateUrl: "./modal-user.component.html",
  styleUrls: ["./modal-user.component.css"],
})
export class ModalUserComponent implements OnInit {
  @Input() public user: UserModel;
  public userForm: FormGroup;

  constructor(
    public dialogRef: MatDialogRef<ModalUserComponent>,
    private userService: UserService,
    private snackBar: MatSnackBar,
    public dialog: MatDialog
  ) {}

  ngOnInit(): void {
    if (this.user === null) {
      this.userForm = new FormGroup({
        name: new FormControl(""),
        surName: new FormControl(""),
        description: new FormControl(""),
        category: new FormControl(0),
      });
    } else {
      this.userForm = new FormGroup({
        name: new FormControl(this.user.name),
        surName: new FormControl(this.user.surName),
        description: new FormControl(this.user.description),
        category: new FormControl(this.user.category),
      });
    }
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  onSubmit(): void {
    if (this.user === null) {
      let newUser = new UserModel();

      newUser.name = this.userForm.value.name;
      newUser.surName = this.userForm.value.surName;
      newUser.description = this.userForm.value.description;
      newUser.category = Number(this.userForm.value.category);

      this.userService.addUser(newUser).subscribe(
        () => {
          this.snackBar.open("Додано користувача", "Приховати", {
            duration: Duration,
          });

          this.dialog.closeAll();
        },
        () => {
          this.snackBar.open("Помилка при додаванні користувача", "Приховати", {
            duration: Duration,
          });
        }
      );
    } else {
      this.user.name = this.userForm.value.name;
      this.user.surName = this.userForm.value.surName;
      this.user.description = this.userForm.value.description;
      this.user.category = Number(this.userForm.value.category);

      console.log(this.user);
      console.log(this.userForm.value);

      this.userService.updateUser(this.user).subscribe(
        () => {
          this.snackBar.open("Дані користувача оновлено", "Приховати", {
            duration: Duration,
          });

          this.dialog.closeAll();
        },
        () => {
          this.snackBar.open(
            "Не вдалося оновити дані користувача",
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
