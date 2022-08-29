import { Component, OnInit, ViewChild } from "@angular/core";
import {
  MatPaginator,
  MatSnackBar,
  MatTableDataSource,
} from "@angular/material";
import { Duration } from "src/app/constants/snackBarDuration";
import { FeedbackModel } from "src/app/models/feedback-model";
import { FeedbackService } from "src/app/services/feedback-service";

@Component({
  selector: "app-feedback-table",
  templateUrl: "./feedback-table.component.html",
  styleUrls: ["./feedback-table.component.css"],
})
export class FeedbackTableComponent implements OnInit {
  private feedbackList: Array<FeedbackModel>;

  public displayedColumns: string[] = ["feedback"];
  public dataSource;

  constructor(
    private snackBar: MatSnackBar,
    private feedbackService: FeedbackService
  ) {}

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;

  ngOnInit() {
    this.feedbackService.getAllFeedbacks().subscribe(
      (data) => {
        this.feedbackList = data;
        this.dataSource = new MatTableDataSource(this.feedbackList);
        this.dataSource.paginator = this.paginator;
      },
      () => {
        this.snackBar.open(
          "Помилка при завантаженні списку відгуків.",
          "Приховати",
          {
            duration: Duration,
          }
        );
      }
    );
  }
}
