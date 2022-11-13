import { Component, Inject, OnInit } from "@angular/core";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { CompanyModel } from "src/app/models/company";


@Component({
  selector: 'app-add-entry-dialog',
  templateUrl: './add-entry-dialog.component.html',
  styleUrls: ['./add-entry-dialog.component.css']
})
export class DialogOverviewExampleDialog implements OnInit{
  constructor(
    public dialogRef: MatDialogRef<DialogOverviewExampleDialog>,
    @Inject(MAT_DIALOG_DATA) public data: CompanyModel,
  ) {}

  ngOnInit(): void {
    
  }
  onNoClick(): void {
    this.dialogRef.close();
  }
}
