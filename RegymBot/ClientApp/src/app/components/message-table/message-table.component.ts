import { Component, OnInit, ViewChild } from "@angular/core";
import {
  MatDialog,
  MatPaginator,
  MatSnackBar,
  MatTableDataSource,
} from "@angular/material";
import { Duration } from "src/app/constants/snackBarDuration";
import { MessageModel } from "src/app/models/message-model";
import { MessageService } from "src/app/services/message-service";

@Component({
  selector: "app-message-table",
  templateUrl: "./message-table.component.html",
  styleUrls: ["./message-table.component.css"],
})
export class MessageTableComponent implements OnInit {
  private messageList: Array<MessageModel>;

  public displayedColumns: string[] = ["page", "message", "actions"];
  public dataSource;

  constructor(
    private snackBar: MatSnackBar,
    public dialog: MatDialog,
    private messageService: MessageService
  ) {}

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;

  ngOnInit() {
    this.messageService.getAllMessages().subscribe(
      (data) => {
        this.messageList = data;
        this.dataSource = new MatTableDataSource(this.messageList);
        this.dataSource.paginator = this.paginator;
      },
      () => {
        this.snackBar.open(
          "Помилка при завантаженні списку повідомлень.",
          "Приховати",
          {
            duration: Duration,
          }
        );
      }
    );
  }

  editMessage(message: MessageModel) {}
}
