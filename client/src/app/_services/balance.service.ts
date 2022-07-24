import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Balance } from '../_models/balance';


@Injectable({
  providedIn: 'root'
})
export class BalanceService {
  baseUrl='http://localhost:5020/';

  constructor(private http: HttpClient) { }

  getBalance(userName: string): Observable<Balance> {
    return this.http.get<Balance>(this.baseUrl + 'balance/' + userName).pipe();
  }
}
