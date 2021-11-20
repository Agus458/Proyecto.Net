import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from 'src/app/services/authentication/authentication.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  constructor(
    public AuthenticationService: AuthenticationService
  ) { }

  ngOnInit(): void {
  }

  logOut(){
    this.AuthenticationService.logOut();
  }

}
