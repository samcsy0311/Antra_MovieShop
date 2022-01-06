import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Login } from 'src/app/shared/models/login';
import { AuthenticationService } from 'src/app/core/services/authentication.service'
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  userLogin: Login = {
    // default values
    email: 'abc@email.com',
    password: '1234qwer'
  };

  constructor(private authService:AuthenticationService, private router:Router) { }

  ngOnInit(): void {
  }

  loginSubmit(form: NgForm) {
    // capture the email/password from the view
    // then send the model to Authentication Service

    // console.log(form);
    // console.log('login button clicked!');
    // console.log(this.userLogin);

    this.authService.login(this.userLogin).subscribe(

      // if token is saved successfully, then redirect to home page
      // if error then show error message and stay on same page

      (response) => {
        if (response) {
          this.router.navigateByUrl('/');
        }

        (err: HttpErrorResponse) => {
          console.log(err);
        }
      }

    );


  }

}
