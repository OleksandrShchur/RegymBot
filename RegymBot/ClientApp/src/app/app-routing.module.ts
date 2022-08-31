import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { FeedbackTableComponent } from "./components/feedback-table/feedback-table.component";
import { LoginComponent } from "./components/login/login.component";
import { MainComponent } from "./components/main/main.component";
import { MessageTableComponent } from "./components/message-table/message-table.component";
import { PriceTableComponent } from "./components/price-table/price-table.component";
import { ScheduleComponent } from "./components/schedule/schedule.component";
import { UserTableComponent } from "./components/user-table/user-table.component";
import { AuthGuard } from "./guards/auth.guard";

const routes: Routes = [{ 
  path: '', 
  component: MainComponent,
  children: [
    {
      path: "message-table", 
      canActivate: [AuthGuard],
      component: MessageTableComponent 
    }, { 
      path: "user-table", 
      canActivate: [AuthGuard],
      component: UserTableComponent 
    }, { 
      path: "price-table", 
      canActivate: [AuthGuard],
      component: PriceTableComponent 
    }, { 
      path: "feedback-table", 
      canActivate: [AuthGuard],
      component: FeedbackTableComponent 
    }, { 
      path: "schedule", 
      canActivate: [AuthGuard],
      component: ScheduleComponent 
    },
    { path: '', redirectTo: 'message-table', pathMatch: 'full' },
  ]
}, 
  { path: 'login', component: LoginComponent },
  { path: '**', redirectTo: '', pathMatch: 'full' },
];

@NgModule({
  declarations: [],
  imports: [CommonModule, RouterModule.forRoot(routes, {enableTracing: true})],
  exports: [RouterModule],
})
export class AppRoutingModule {}
