import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Orders } from '../_models/orders';
import { Products } from '../_models/products';
import { User } from '../_models/user';
import { AccountService } from '../_services/account.service';
import { BalanceService } from '../_services/balance.service';
import { OrdersService } from '../_services/orders.service';
import { ProductService } from '../_services/product.service';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.scss']
})
export class OrdersComponent implements OnInit {

  orders: Orders[] = []; 
  userName :string ;
  allProducts: Products[] = [];

  constructor(private http: HttpClient, 
    private ordersService: OrdersService, 
    private productService: ProductService,
    public accountService: AccountService) {
    this.accountService.currentUser$.subscribe(response => this.userName = response.userName);
    
   }

  ngOnInit(): void {
    this.productService.getProducts().subscribe(products => {
      this.allProducts = products;
      this.getOrders(this.userName);
    });
  }

  getOrders(userName: string) {
    return this.ordersService.getOrders(userName).subscribe(orders => {
      this.orders = orders;
      this.orders.forEach(o => {
        o.imgUrl = this.allProducts.find(p => p.name === o.productName).imgUrl;
      });
    });
  }


}
