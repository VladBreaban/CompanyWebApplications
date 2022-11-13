import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { CompanyModel } from 'src/app/models/company';
import { DataServiceService } from 'src/app/services/data-service.service';
import { DialogOverviewExampleDialog } from '../add-entry-dialog/add-entry-dialog.component';
import { MatSnackBar } from '@angular/material/snack-bar';

export class DialogData {
  company: CompanyModel = new CompanyModel();
  isEdit: boolean =false;
}


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
  constructor(public dialog: MatDialog, private dataService: DataServiceService, private _snackBar: MatSnackBar) { }

  ngOnInit(): void {
    this.populateDataSource();
  }
  populateDataSource() : void{
    this.companies = [];
    this.dataService.getCompanies().subscribe(x=>{
      this.companies =x;
    });
  };

  OpenAddCompanyModal(): void {
    const dialogRef = this.dialog.open(DialogOverviewExampleDialog, {
      width: '450px',
      data: {company:this.companyToBeAdded, isEdit:false},
      disableClose:true
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      //ading created result to table
      if(result != null)
      {
        this.dataService.createUpdateCompany(result,false).subscribe(x=>{
          this.populateDataSource();
          this.companyToBeAdded = new CompanyModel();
 
        },
        error=>{
          console.log(error);
          this._snackBar.open('Cannot create company. Reason: ' + error.error, 'Close', {
            horizontalPosition: "right",
            verticalPosition: "top",
            duration: 3000
        });

        }
      );
      }
    });
  }
}
