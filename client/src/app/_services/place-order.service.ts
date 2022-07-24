import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Products } from '../_models/products';

@Injectable({
  providedIn: 'root'
})
export class PlaceOrderService {
  baseUrl='http://localhost:5020/';
  constructor(private http: HttpClient) { }

  placeOrder(product: any)  {
    return this.http.post(this.baseUrl + 'marketplace/placeorder', product);
  }
}
