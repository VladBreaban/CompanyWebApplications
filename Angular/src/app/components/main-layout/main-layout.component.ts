import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-main-layout',
  templateUrl: './main-layout.component.html',
  styleUrls: ['./main-layout.component.css']
})
export class MainLayoutComponent implements OnInit {
  currentEmail: string | null = "";

  constructor(private router: Router) { }

  ngOnInit(): void {
    this.currentEmail = localStorage.getItem('userEmail');

  }

  logout():void{
    localStorage.clear();
    this.router.navigate(["/login"]);

  }

}
