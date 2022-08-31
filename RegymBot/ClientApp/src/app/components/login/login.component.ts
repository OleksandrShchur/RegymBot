import { Component, OnInit } from '@angular/core';
import { FormGroup, AbstractControl, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ValidatorsExtensions } from 'src/app/helpers/validators';
import { AuthService } from 'src/app/services/auth.service';
import { NotificationsService } from 'src/app/services/notifications.service';

@Component({
    selector: 'doct-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss']
})
export class LoginComponent {
    authGroup: FormGroup;
    hide: boolean = true;
    authControl(value: string): AbstractControl | null { return this.authGroup ? this.authGroup.get(value) : null; }
    loading: boolean = false;
    constructor(
        public authService: AuthService,
        public notifications: NotificationsService,
        private router: Router,
    ) {
        console.log('open login')
        if (this.authService.authorized) {
            this.router.navigate(['']);
        }
        this.authGroup = new FormGroup({
            loginControl: new FormControl('', [Validators.required, ValidatorsExtensions.empty]),
            passwordControl: new FormControl('', [Validators.required])
        });
    }

    async auth() {
        this.authGroup.markAllAsTouched();
        this.authGroup.updateValueAndValidity();
        if (!this.authGroup.valid) {
            return;
        }
        this.authGroup.value;
        this.loading = true;
        try {
            let res = await this.authService.auth(
                this.authControl('loginControl').value,
                this.authControl('passwordControl').value
            );
            if (res) {
                this.router.navigate(['']);
            } else {
                this.notifications.error('Невiрний логiн чи пароль.');
            }
        } catch (e) {
            this.notifications.error('Невiрний логiн чи пароль.');
            console.log(e);
        }
        this.loading = false;
    }
}
