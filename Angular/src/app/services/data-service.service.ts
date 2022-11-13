import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { CompanyModel } from '../models/company';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class DataServiceService {

  constructor(private http: HttpClient, private _snackBar: MatSnackBar) { }

   getCompanies() {
    let jwt = localStorage.getItem('jwt');
    var reqHeader = new HttpHeaders({ 
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + jwt
     });
    var response =  this.http.get<CompanyModel[]>(environment.urlServices + "Company/GetAllCompanies/", { headers: reqHeader });
    return response;    
  }   

   getPropertyById(companyId: number) {
    let jwt = localStorage.getItem('jwt');
    var reqHeader = new HttpHeaders({ 
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + jwt
     });
    var response = this.http.get<CompanyModel>(environment.urlServices + "Company/GetCompanyById/" + companyId.toString(), { headers: reqHeader });
    return response;
  }

  createUpdateCompany(model: CompanyModel, isUpdate:boolean) {
    let jwt = localStorage.getItem('jwt');
    var url = environment.urlServices;
    if(isUpdate == true)
    {
      url = url + "Company/UpdateCompany";
    } else{
       url = url + "Company/CreateCompany";
    }
    return this.http.post<CompanyModel>(url, model, {
        headers: new HttpHeaders({ "Content-Type": "application/json", 'Authorization': 'Bearer ' + jwt })
    })
        .subscribe({
            error: (err: HttpErrorResponse) => {
                this._snackBar.open('Cannot create contract' + err.message, 'Close', {
                    horizontalPosition: "right",
                    verticalPosition: "top",
                    duration: 3000
                });
            }
        })
  }




}
