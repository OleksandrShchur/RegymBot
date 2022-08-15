import { Component, Input, OnInit } from "@angular/core";
import { FormControl, FormGroup } from "@angular/forms";
import { MatDialog, MatDialogRef, MatSnackBar } from "@angular/material";
import { Duration } from "src/app/constants/snackBarDuration";
import { MessageModel } from "src/app/models/message-model";
import { MessageService } from "src/app/services/message-service";

@Component({
  selector: "app-modal-message",
  templateUrl: "./modal-message.component.html",
  styleUrls: ["./modal-message.component.css"],
})
export class ModalMessageComponent implements OnInit {
  @Input() public message: MessageModel;
  public messageForm: FormGroup;

  constructor(
    public dialogRef: MatDialogRef<ModalMessageComponent>,
    private messageService: MessageService,
    private snackBar: MatSnackBar,
    public dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.messageForm = new FormGroup({
      page: new FormControl(this.message.page),
      message: new FormControl(this.message.message),
    });
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  onSubmit(): void {
    this.message.message = this.messageForm.value.message;

    this.messageService.updateMessage(this.message).subscribe(
      () => {
        this.snackBar.open("Повідомлення оновлено", "Приховати", {
          duration: Duration,
        });

        this.dialog.closeAll();
      },
      () => {
        this.snackBar.open("Не вдалося оновити повідомлення", "Приховати", {
          duration: Duration,
        });
      }
    );
  }
}
