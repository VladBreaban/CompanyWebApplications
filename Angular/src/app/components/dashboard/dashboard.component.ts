import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { CompanyModel } from 'src/app/models/company';
import { DataServiceService } from 'src/app/services/data-service.service';
import { DialogOverviewExampleDialog } from '../add-entry-dialog/add-entry-dialog.component';


@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  columndefs : any[] = ['name','exchange', 'ticker', 'isin', 'website', 'edit'];
  public companies: CompanyModel[]=[];
  isCompanyModalVisible: boolean = false;
  companyToBeAdded: CompanyModel = new CompanyModel();
  constructor(public dialog: MatDialog, private dataService: DataServiceService) { }

  ngOnInit(): void {
    this.dataService.getCompanies().subscribe(x=>{
      this.companies =x;
    });
  }

  OpenCompanyModal(): void {
    const dialogRef = this.dialog.open(DialogOverviewExampleDialog, {
      width: '450px',
      data: this.companyToBeAdded,
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      //ading created result to table
      if(result != null)
      {
        this.companies.push(result);
      }
    });
  }
}
