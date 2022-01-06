import { Component, OnInit } from '@angular/core';
import { User } from '../../../shared/models/user';
import { AuthenticationService } from '../../../core/services/authentication.service'
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  isLoggedIn: boolean = false;
  user!: User;
  elseBlock:any = true;

  constructor(private authService: AuthenticationService, private router: Router) {}

  ngOnInit(): void{
    this.authService.isLoggedIn.subscribe(resp => this.isLoggedIn = resp);
    this.authService.currentUser.subscribe(resp => this.user = resp);
  }

  logout() {
    this.authService.logout();
    this.router.navigateByUrl('account/login');
  }

}
