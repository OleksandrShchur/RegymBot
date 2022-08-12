import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { HomeComponent } from "./components/home/home.component";
import { PriceServiceModuleComponent } from "./components/price-service-module/price-service-module.component";
import { UserTableComponent } from "./components/user-table/user-table.component";

const routes: Routes = [
  { path: "", component: HomeComponent },
  { path: "user-table", component: UserTableComponent },
  { path: "price-service", component: PriceServiceModuleComponent },
];

@NgModule({
  declarations: [],
  imports: [CommonModule, RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
