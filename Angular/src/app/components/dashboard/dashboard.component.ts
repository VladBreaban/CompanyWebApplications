import { Component, OnInit } from '@angular/core';
import { CompanyModel } from 'src/app/models/company';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  public companies: CompanyModel[]=[{name: "Company1", exchange: "Exchange1", ticker:"TCK", website:"www.website.com", isin:"US123456789"}];
  constructor() { }

  ngOnInit(): void {
  }

}
