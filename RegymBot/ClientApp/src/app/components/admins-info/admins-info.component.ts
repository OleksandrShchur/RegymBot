import { Component, OnInit } from "@angular/core";
import { FormControl, FormGroup } from "@angular/forms";
import {
  MatSnackBar,
} from "@angular/material";
import { Duration } from "src/app/constants/snackBarDuration";
import { AdminsInfo } from "src/app/models/admins-info";
import { AdminsRegistrationLinks } from "src/app/models/admins-registration-links";
import { AdminsInfoService } from "src/app/services/admins-info-service";

@Component({
  selector: "app-admins-info",
  templateUrl: "./admins-info.component.html",
  styleUrls: ["./admins-info.component.scss"],
})
export class AdminsInfoComponent implements OnInit {
  adminsRegistrationLinks: AdminsRegistrationLinks;
  adminsGroup = this.generateGroup();

  constructor(
    private snackBar: MatSnackBar,
    private adminsInfoService: AdminsInfoService,
  ) {}
    
  ngOnInit(): void {
    this.adminsInfoService.getAdminsRegistrationLinks().subscribe(
      (data) => {
        this.adminsRegistrationLinks = data;
      },
      () => {
        this.snackBar.open(
          "Помилка при завантаженні посилань реестрації.",
          "Приховати",
          { duration: Duration }
        );
      }
    );

    this.adminsInfoService.get().subscribe(
      (data) => {
        this.adminsGroup = this.generateGroup(data);
      },
      () => {
        this.snackBar.open(
          "Помилка при завантаженні логінів адмінів.",
          "Приховати",
          { duration: Duration }
        );
      }
    );
  }

  generateGroup(adminsInfo?: AdminsInfo) {
    return new FormGroup({ 
      adminApolloLogin: new FormControl(adminsInfo ? adminsInfo.adminApolloLogin : ''),
      adminVavylonLogin: new FormControl(adminsInfo ? adminsInfo.adminVavylonLogin : ''),
      adminPSHKNLogin: new FormControl(adminsInfo ? adminsInfo.adminPSHKNLogin : ''),

      // we cannot modify this values for now, but keep in group for proper forms values
      adminApolloTelegramId: new FormControl({ 
        value: adminsInfo ? adminsInfo.adminApolloTelegramId : 0, 
        disabled: true 
      }),
      adminVavylonTelegramId: new FormControl({ 
        value: adminsInfo ? adminsInfo.adminVavylonTelegramId : 0, 
        disabled: true 
      }),
      adminPSHKNTelegramId: new FormControl({ 
        value: adminsInfo ? adminsInfo.adminPSHKNTelegramId : 0, 
        disabled: true 
      }),
    })
  }
    
  onUpdate(): void {
    const model: AdminsInfo = this.adminsGroup.getRawValue();

    this.adminsInfoService.update(model).subscribe(
      () => {
        this.snackBar.open("Адмінів оновлено", "Приховати", {
          duration: Duration,
      });
      },
      () => {
        this.snackBar.open("Не вдалося оновити Адмінів", "Приховати", {
          duration: Duration,
        });
      }
    );
  }
}
