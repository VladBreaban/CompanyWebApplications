import { animate, state, style, transition, trigger } from '@angular/animations';
import { Component, OnInit } from '@angular/core';
import { LoginModel } from 'src/app/models/login';
import { SignupModel } from 'src/app/models/signup';
import { AuthenticationService } from 'src/app/services/authentication-service.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  animations: [
    trigger('flipState', [
      state('active', style({
        transform: 'rotateY(179deg)'
      })),
      state('inactive', style({
        transform: 'rotateY(0)'
      })),
      transition('active => inactive', animate('400ms ease-out')),
      transition('inactive => active', animate('400ms ease-in'))
    ])
  ]
})
export class LoginComponent implements OnInit {

  constructor(private loginService: AuthenticationService) { }
   public model!:LoginModel;
   public signupModel!:SignupModel;
   flip: string = 'inactive';
   validPass = true;

  toggleFlip() {
    this.flip = (this.flip == 'inactive') ? 'active' : 'inactive';
  }

  ngOnInit(): void {
   this.model = new LoginModel();
    this.model.email = '';
    this.model.password ='';

    this.signupModel = new SignupModel();
    this.signupModel.email = '';
    this.signupModel.password = '';
    this.signupModel.passwordConfirm = '';
  }
  handleClick(event: Event) { 
    this.loginService.login(this.model);
    this.model = new LoginModel();
  } 

  createAccount(event: Event){
    if(this.signupModel.password == this.signupModel.passwordConfirm){
      this.validPass = true;
      let newModel = new LoginModel();
      newModel.email = this.signupModel.email;
      newModel.password = this.signupModel.password;
      this.loginService.register(newModel);
      this.toggleFlip();
    }
    else{
      this.validPass = false;
      return;
    }
  }
}
