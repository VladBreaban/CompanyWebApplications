import { Dialog } from "@angular/cdk/dialog";
import { Component, Inject, Input, OnInit } from "@angular/core";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { CompanyModel } from "src/app/models/company";
import { DataServiceService } from "src/app/services/data-service.service";
import { DialogData } from "../dashboard/dashboard.component";


@Component({
  selector: 'app-add-entry-dialog',
  templateUrl: './add-entry-dialog.component.html',
  styleUrls: ['./add-entry-dialog.component.css']
})
export class DialogOverviewExampleDialog implements OnInit{
  constructor(
    public dialogRef: MatDialogRef<DialogOverviewExampleDialog>,
    private dataService: DataServiceService,
    @Inject(MAT_DIALOG_DATA) public data: DialogData,
  ) {}

  ngOnInit(): void {
   if(this.data.isEdit === true)
   {
       this.dataService.getCompanyById(this.data.potentialCompanyId).subscribe(x=>{
          this.data.company = x;
       });
   }
  }
  onNoClick(): void {
    this.dialogRef.close();
    this.data = new DialogData();
  }
}
