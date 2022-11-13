import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
// import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { environment } from 'src/environments/environment';
import { LoginModel } from '../models/login';
import{AuthenticatedResponse} from '../models/auth-response.model'

@Injectable({
    providedIn: 'root',
})
export class AuthenticationService {

    constructor(
        private router: Router,
        private jwtHelper: JwtHelperService,
        private http: HttpClient,) { }

    isUserAuthenticated = (): boolean => {
        const token = localStorage.getItem("jwt");
        if (token && !this.jwtHelper.isTokenExpired(token)) {
            return true;
        }
        return false;
    }
    logOut = () => {
        localStorage.removeItem("jwt");
        localStorage.removeItem("userEmail");
        localStorage.removeItem("userId");
    }

    login(credentials: LoginModel) {
        this.http.post<AuthenticatedResponse>(environment.urlServices+"Auth/login", credentials, {
            headers: new HttpHeaders({ "Content-Type": "application/json" })
        })
            .subscribe({
                next: (response: AuthenticatedResponse) => {
                    const token = response.token;
                    const email = response.email;
                    localStorage.setItem("jwt", token);
                    localStorage.setItem("userEmail", email);
                    this.router.navigate(["/main/dashboard"]);
                },
                error: (err: HttpErrorResponse) => {

                    // this._snackBar.open('Email sau parola gresite', 'Close', {
                    //     horizontalPosition: "right",
                    //     verticalPosition: "top",
                    //     duration: 3000
                    // });
                }
            })
    }
}