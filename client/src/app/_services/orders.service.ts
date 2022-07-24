import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Orders } from '../_models/orders';

@Injectable({
  providedIn: 'root'
})
export class OrdersService {
  baseUrl='http://localhost:5020/';


  constructor(private http: HttpClient) { }

  getOrders(userName: string): Observable<Orders[]> {
    return this.http.get<Orders[]>(this.baseUrl + 'order/' + userName);
  }
}
