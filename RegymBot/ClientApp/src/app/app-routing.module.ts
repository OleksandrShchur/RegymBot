import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { MessageTableComponent } from "./components/message-table/message-table.component";
import { PriceTableComponent } from "./components/price-table/price-table.component";
import { UserTableComponent } from "./components/user-table/user-table.component";

const routes: Routes = [
  { path: "", component: MessageTableComponent },
  { path: "user-table", component: UserTableComponent },
  { path: "price-table", component: PriceTableComponent },
];

@NgModule({
  declarations: [],
  imports: [CommonModule, RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
