import { Component, ViewChild } from "@angular/core";
import { BreakpointObserver } from "@angular/cdk/layout";
import { MatSidenav } from "@angular/material/sidenav";
import { AuthService } from "src/app/services/auth.service";

@Component({
  selector: "app-sidebar",
  templateUrl: "./sidebar.component.html",
  styleUrls: ["./sidebar.component.scss"],
})
export class SidebarComponent {
  @ViewChild(MatSidenav, { static: true })
  sidenav: MatSidenav;

  constructor(
    private observer: BreakpointObserver,
    private auth: AuthService,
    ) {}

  ngOnInit() {}

  ngAfterViewInit() {
    this.observer.observe(["(max-width: 800px)"]).subscribe((res) => {
      if (res.matches) {
        this.sidenav.mode = "over";
        this.sidenav.close();
      } else {
        this.sidenav.mode = "side";
        this.sidenav.open();
      }
    });
  }

  logout() { 
    this.auth.logout();
  }
}
