import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { HubConnectionBuilder, HubConnection } from '@microsoft/signalr';
import { Balance } from '../_models/balance';
import { AccountService } from './account.service';
import { BalanceService } from './balance.service';

@Injectable({
  providedIn: 'root'
})
export class SignalrService {
  hubUrl = 'https://localhost:5001/signalServer/balance';
  userName: string;
  balance: Balance;
  
  private hubConnection: HubConnection;

  public startConnection = () => {
    this.hubConnection = new HubConnectionBuilder().withUrl(this.hubUrl).build();
    this.hubConnection.start()
    .then(() => console.log('connection started'))
    .catch(err => console.log('Error while starting connections: ' + err))
  }

  public updateBalance()  {
    console.log("look at me");
    this.hubConnection.on('refreshBalance', (balance) => {
      this.balance = balance;
      this.getBalance();
      console.log("**balance: ");
      
    })
    //return this.balance.balance;
  }

  constructor(private balanceService: BalanceService, private accountService: AccountService) { }

  /*
  createHubConnection()
  {
    this.hubConnection = new HubConnectionBuilder().withUrl(this.hubUrl).build();
    this.hubConnection.start()
    .catch(error => console.log(error));
    
    //this.hubConnection.on("refreshBalance", this.getBalance());
    this.hubConnection.on("refreshBalance", (balance) =>{
      this.balance= balance;
      console.log(balance);
    });
  }

  stopHubConnection(){
    this.hubConnection.stop().catch(error => console.log(error));
  }*/
  getUserName(){
    this.accountService.currentUser$.subscribe(response => this.userName = response.userName)
  }

  getBalance()
  {
    this.getUserName();
    this.balanceService.getBalance(this.userName).subscribe();
  }
}
