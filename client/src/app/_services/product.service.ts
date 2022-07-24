import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { Products } from '../_models/products';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  baseUrl='http://localhost:5020/';

  constructor(private http: HttpClient) { }

  getProducts(): Observable<Products[]>{
    return this.http.get<Products[]>(this.baseUrl + 'product').pipe();
  }
}
