import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../_models/user';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {}
 
  constructor(public accountService: AccountService) { }
  currentUser: any;

  ngOnInit(): void {
   this.currentUser = localStorage.getItem('user');
  }

  login(){
    this.accountService.login(this.model).subscribe(response => {
      console.log(response);
      this.currentUser = localStorage.getItem('user');
    }, error => {
      console.log(error);
    });
  }
  logout(){
    this.accountService.logout();
    this.currentUser = null;
  }
}
