import { Component, Input, OnInit } from '@angular/core';
import { PlaceOrderService } from '../_services/place-order.service';
import { AccountService } from '../_services/account.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-item-card',
  templateUrl: './item-card.component.html',
  styleUrls: ['./item-card.component.scss']
})
export class ItemCardComponent implements OnInit {
 @Input() productsToShow: any;
 userName :string ;

  constructor(public placeOrderService: PlaceOrderService, public accountService: AccountService,
     private router: Router) { 
    this.accountService.currentUser$.subscribe(response => this.userName = response.userName);
  }

  ngOnInit(): void {
  }

  placeOrder(product: any){
    let body =
    {
      Name: product.name,
      UserName: this.userName,
    }

    this.placeOrderService.placeOrder(body).subscribe(response => {
      console.log("**respone** "+ response);
      alert("Balance may change, please refresh to see the update balance")
      this.router.navigateByUrl('/orders');
    }, error => console.log(error))
    
    
  }

  

}
