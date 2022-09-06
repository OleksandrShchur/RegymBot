import { Component, Input, OnInit } from "@angular/core";
import { FormControl, FormGroup } from "@angular/forms";
import { MatDialog, MatDialogRef, MatSnackBar } from "@angular/material";
import { MatInputModule } from "@angular/material";
import { Duration } from "src/app/constants/snackBarDuration";
import { ImageSnippet } from "src/app/helpers/imageSnippet";
import { UserModel } from "src/app/models/user-model";
import { UserService } from "src/app/services/user-service";
import { v4 as uuid } from "uuid";

@Component({
  selector: "app-modal-user",
  templateUrl: "./modal-user.component.html",
  styleUrls: ["./modal-user.component.scss"],
})
export class ModalUserComponent implements OnInit {
  @Input() public user: UserModel;
  public userForm: FormGroup;
  public selectedFile: ImageSnippet;

  constructor(
    public dialogRef: MatDialogRef<ModalUserComponent>,
    private userService: UserService,
    private snackBar: MatSnackBar,
    public dialog: MatDialog
  ) {}

  ngOnInit(): void {
    if (this.user === null) {
      this.userForm = new FormGroup({
        imageUrl: new FormControl(""),
        name: new FormControl(""),
        surName: new FormControl(""),
        description: new FormControl(""),
        category: new FormControl(0),
      });
    } else {
      this.userForm = new FormGroup({
        imageUrl: new FormControl(this.user.imageUrl),
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
    try {
      console.log(this.selectedFile);
      const imgFile =
        this.selectedFile === undefined ? null : this.selectedFile.file;

      if (this.user === null) {
        let newUser = new UserModel();

        newUser.name = this.userForm.value.name;
        newUser.surName = this.userForm.value.surName;
        newUser.description = this.userForm.value.description;
        newUser.category = Number(this.userForm.value.category);

        newUser.userGuid = uuid.v4();

        this.userService.addUser(newUser).subscribe(
          () => {
            if (imgFile !== null) {
              this.userService
                .uploadUserAvatar(imgFile, newUser.userGuid)
                .subscribe(
                  () => {
                    this.snackBar.open("Додано користувача", "Приховати", {
                      duration: Duration,
                    });

                    this.dialog.closeAll();
                  },
                  () => {
                    this.snackBar.open(
                      "Помилка при додаванні користувача",
                      "Приховати",
                      {
                        duration: Duration,
                      }
                    );
                  }
                );
            } else {
              this.snackBar.open("Додано користувача без фото", "Приховати", {
                duration: Duration,
              });

              this.dialog.closeAll();
            }
          },
          () => {
            this.snackBar.open(
              "Помилка при додаванні користувача",
              "Приховати",
              {
                duration: Duration,
              }
            );
          }
        );
      } else {
        this.user.name = this.userForm.value.name;
        this.user.surName = this.userForm.value.surName;
        this.user.description = this.userForm.value.description;
        this.user.category = Number(this.userForm.value.category);

        this.userService.updateUser(this.user).subscribe(
          () => {
            if (imgFile !== null) {
              this.userService
                .uploadUserAvatar(imgFile, this.user.userGuid)
                .subscribe(
                  () => {
                    this.snackBar.open(
                      "Дані користувача та фото оновлено",
                      "Приховати",
                      {
                        duration: Duration,
                      }
                    );

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
            } else {
              this.snackBar.open("Дані користувача оновлено", "Приховати", {
                duration: Duration,
              });
            }
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

      this.dialog.closeAll();
    } catch (e) {
      alert(e.message);
    }
  }

  processFile(imageInput: any) {
    try {
      const file: File = imageInput.files[0];
      const reader = new FileReader();

      reader.addEventListener("load", (event: any) => {
        this.selectedFile = new ImageSnippet(event.target.result, file);
        this.selectedFile.pending = true;
      });

      reader.readAsDataURL(file);
    } catch (e) {
      alert(e.message);
    }
  }
}
