import { Component, OnInit } from '@angular/core';
import { User } from './_models/user';
import { AccountService } from './_services/account.service';
import { BalanceService } from './_services/balance.service';
import { SignalrService } from './_services/signalr.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  title = 'client';
  userName: string;
  balance: number;


  constructor(private accountService:AccountService, private signalRService: SignalrService,
    private balanceService: BalanceService) {}

  ngOnInit() {
    this.setCurrentUser();

    this.getUserName();
    this.getBalance(this.userName);
  }

  setCurrentUser(){
    const user:User = JSON.parse(localStorage.getItem('user'));
    this.accountService.setCurrentUser(user);
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
}
