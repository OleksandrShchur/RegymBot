import { BrowserModule } from "@angular/platform-browser";
import { LOCALE_ID, NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";

import { AppComponent } from "./app.component";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { SidebarComponent } from "./components/sidebar/sidebar.component";
import {
  MatButtonModule,
  MatDialogModule,
  MatDividerModule,
  MatFormFieldModule,
  MatIconModule,
  MatInputModule,
  MatPaginatorModule,
  MatProgressBarModule,
  MatProgressSpinnerModule,
  MatSelectModule,
  MatSidenavModule,
  MatSnackBarModule,
  MatTableModule,
  MatToolbarModule,
} from "@angular/material";
import { UserTableComponent } from "./components/user-table/user-table.component";
import { AppRoutingModule } from "./app-routing.module";
import { UserService } from "./services/user-service";
import { PriceService } from "./services/price-service";
import { ModalUserComponent } from "./components/modal-user/modal-user.component";
import { MessageTableComponent } from "./components/message-table/message-table.component";
import { PriceTableComponent } from "./components/price-table/price-table.component";
import { ModalPriceComponent } from "./components/modal-price/modal-price.component";
import { MessageService } from "./services/message-service";
import { ModalMessageComponent } from "./components/modal-message/modal-message.component";
import { FeedbackTableComponent } from "./components/feedback-table/feedback-table.component";
import { FeedbackService } from "./services/feedback-service";
import { ScheduleComponent } from "./components/schedule/schedule.component";
import { ScheduleService } from "./services/schedule-service";
import { LoginComponent } from "./components/login/login.component";
import { NotificationsService } from "./services/notifications.service";
import { AuthService } from "./services/auth.service";
import { JwtInterceptor } from "./helpers/auth.interceptor";
import { MainComponent } from "./components/main/main.component";

@NgModule({
  declarations: [
    AppComponent,
    MainComponent,
    LoginComponent,
    SidebarComponent,
    UserTableComponent,
    ModalUserComponent,
    MessageTableComponent,
    PriceTableComponent,
    ModalPriceComponent,
    ModalMessageComponent,
    FeedbackTableComponent,
    ScheduleComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    MatSidenavModule,
    MatToolbarModule,
    MatIconModule,
    MatDividerModule,
    MatTableModule,
    MatSnackBarModule,
    MatPaginatorModule,
    MatProgressBarModule,
    MatProgressSpinnerModule,
    MatButtonModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    FormsModule,
    ReactiveFormsModule,
    MatSelectModule,
  ],
  entryComponents: [
    ModalUserComponent,
    ModalPriceComponent,
    ModalMessageComponent,
  ],
  providers: [
    AuthService,
    NotificationsService,
    UserService,
    PriceService,
    MessageService,
    FeedbackService,
    ScheduleService,
    { provide: LOCALE_ID, useValue: "uk-UA" },
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true }
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
