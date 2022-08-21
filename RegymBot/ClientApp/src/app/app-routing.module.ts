import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { FeedbackTableComponent } from "./components/feedback-table/feedback-table.component";
import { MessageTableComponent } from "./components/message-table/message-table.component";
import { PriceTableComponent } from "./components/price-table/price-table.component";
import { UserTableComponent } from "./components/user-table/user-table.component";

const routes: Routes = [
  { path: "", component: MessageTableComponent },
  { path: "user-table", component: UserTableComponent },
  { path: "price-table", component: PriceTableComponent },
  { path: "feedback-table", component: FeedbackTableComponent },
];

@NgModule({
  declarations: [],
  imports: [CommonModule, RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
