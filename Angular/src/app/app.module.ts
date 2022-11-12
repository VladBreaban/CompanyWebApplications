import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthGuard } from './guards/auth.guard';
import { LoginComponent } from './components/login/login.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { MainLayoutComponent } from './components/main-layout/main-layout.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatCardModule } from '@angular/material/card';
import {MatDividerModule} from '@angular/material/divider';
import {getMatFormFieldMissingControlError, MatFormFieldModule} from '@angular/material/form-field';
import { JwtHelperService, JWT_OPTIONS  } from '@auth0/angular-jwt';
import { HttpClient } from '@angular/common/http';
import { HttpClientModule } from '@angular/common/http';
import { MatInputModule } from '@angular/material/input';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    DashboardComponent,
    MainLayoutComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatCardModule,
    MatDividerModule,
    MatFormFieldModule,
    FormsModule,
    HttpClientModule,
    MatInputModule
  ],
  providers: [AuthGuard,       
     { provide: JWT_OPTIONS, useValue: JWT_OPTIONS },
    JwtHelperService, HttpClient],
  bootstrap: [AppComponent]
})
export class AppModule { }
