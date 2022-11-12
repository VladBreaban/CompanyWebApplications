import { Component, OnInit } from '@angular/core';
import { LoginModel } from 'src/app/models/login';
import { AuthenticationService } from 'src/app/services/authentication-service.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private loginService: AuthenticationService) { }
   public model!:LoginModel;

   
  ngOnInit(): void {
   this.model = new LoginModel();
    this.model.email = '';
    this.model.password ='';
  }
  handleClick(event: Event) { 
    this.loginService.login(this.model);
  } 
}
