import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { HttpClientModule } from "@angular/common/http";
import { RouterModule } from "@angular/router";

import { AppComponent } from "./app.component";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { HomeComponent } from "./components/home/home.component";
import { SidebarComponent } from "./components/sidebar/sidebar.component";
import {
  MatButtonModule,
  MatDialogModule,
  MatDividerModule,
  MatFormFieldModule,
  MatIconModule,
  MatInputModule,
  MatPaginatorModule,
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

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    SidebarComponent,
    UserTableComponent,
    ModalUserComponent,
    MessageTableComponent,
    PriceTableComponent,
    ModalPriceComponent,
    ModalMessageComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: "ng-cli-universal" }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: "", component: HomeComponent, pathMatch: "full" },
    ]),
    BrowserAnimationsModule,
    AppRoutingModule,
    MatSidenavModule,
    MatToolbarModule,
    MatIconModule,
    MatDividerModule,
    MatTableModule,
    MatSnackBarModule,
    MatPaginatorModule,
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
  providers: [UserService, PriceService, MessageService],
  bootstrap: [AppComponent],
})
export class AppModule {}
