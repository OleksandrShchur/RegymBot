import { Component, OnInit } from "@angular/core";
import { MatSnackBar } from "@angular/material";
import { Duration } from "src/app/constants/snackBarDuration";
import { ImageSnippet } from "src/app/helpers/imageSnippet";
import { ScheduleService } from "src/app/services/schedule-service";

@Component({
  selector: "app-schedule",
  templateUrl: "./schedule.component.html",
  styleUrls: ["./schedule.component.scss"],
})
export class ScheduleComponent implements OnInit {
  public selectedFile: ImageSnippet;

  constructor(
    private snackBar: MatSnackBar,
    private scheduleService: ScheduleService
  ) {}

  ngOnInit() {}

  processFile(imageInput: any, clubIndex: number) {
    const file: File = imageInput.files[0];
    const reader = new FileReader();

    reader.addEventListener("load", (event: any) => {
      this.selectedFile = new ImageSnippet(event.target.result, file);
      this.selectedFile.pending = true;

      this.scheduleService
        .uploadScheduleImage(this.selectedFile.file, clubIndex)
        .subscribe(
          (data) => {
            this.selectedFile.pending = false;
            this.selectedFile.status = "ok";
          },
          () => {
            this.selectedFile.pending = false;
            this.selectedFile.status = "fail";
            this.selectedFile.src = "";

            this.snackBar.open(
              "Помилка при завантаженні зображення",
              "Приховати",
              {
                duration: Duration,
              }
            );
          }
        );
    });

    reader.readAsDataURL(file);
  }
}
