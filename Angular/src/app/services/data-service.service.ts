import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { CompanyModel } from '../models/company';
// import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class DataServiceService {

  constructor(private http: HttpClient) { }

   getCompanies() {
    let jwt = localStorage.getItem('jwt');
    var reqHeader = new HttpHeaders({ 
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + jwt
     });
    var response =  this.http.get<CompanyModel[]>(environment.urlServices + "Company/GetAllCompanies", { headers: reqHeader });
    return response;    
  }   

   getCompanyById(companyId: string) {
    let jwt = localStorage.getItem('jwt');
    var reqHeader = new HttpHeaders({ 
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + jwt
     });
    var response = this.http.get<CompanyModel>(environment.urlServices + "Company/GetCompanyById/" + companyId.toString(), { headers: reqHeader });
    return response;
  }

  createUpdateCompany(model: CompanyModel, isUpdate:boolean) :Observable<CompanyModel> {
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
    });
  }




}
