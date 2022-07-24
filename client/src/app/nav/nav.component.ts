import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Balance } from '../_models/balance';

import { AccountService } from '../_services/account.service';
import { BalanceService } from '../_services/balance.service';
import { SignalrService } from '../_services/signalr.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})
export class NavComponent implements OnInit {
  model: any ={}
  userName: string;
  balance: number;


  constructor(public accountService: AccountService, private router: Router, private balanceService: BalanceService
    ,private signalRService: SignalrService) {
    
   }

  ngOnInit(): void {
    //this.signalRService.startConnection();
    //this.signalRService.updateBalance();
    this.getUserName();
    this.getBalance(this.userName);
  }
  
login() {
  this.accountService.login(this.model).subscribe(response => {
    this.getUserName();
    this.getBalance(this.userName);
    this.router.navigateByUrl('/'); //navigate to the home page after we login
  }, error => {
    alert("Username or Password incorrect");
    console.log(error);
  })
}

logout(){
  this.accountService.logout();
  this.router.navigateByUrl('/');
}

getBalance(userName: string){
  return this.balanceService.getBalance(userName).subscribe(response => {
    if(response){
    this.balance = response.balance}
    else
    {this.balance = 0}
  });
}
getUserName(){
  this.accountService.currentUser$.subscribe(response => this.userName = response.userName)
}

realodBalance(){
  location.reload();
}
}
