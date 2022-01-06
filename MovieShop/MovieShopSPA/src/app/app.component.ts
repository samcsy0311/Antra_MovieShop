import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from './core/services/authentication.service';
import { User } from './shared/models/user';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'MovieShop SPA';
  test = 'Some data';
  isLoggedIn: boolean = false;
  user!: User;
  elseBlock:any = true;

  constructor(private authService: AuthenticationService, private router: Router) {}

  ngOnInit(): void{
    this.authService.isLoggedIn.subscribe(resp => this.isLoggedIn = resp);
    this.authService.currentUser.subscribe(resp => this.user = resp);

    this.authService.populateUserInfo();
  }

  logout() {
    this.authService.logout();
    this.router.navigateByUrl('account/login');
  }
}
