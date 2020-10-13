import { Injectable } from '@angular/core';
import { HttpClient ,HttpHeaders} from '@angular/common/http';
import 'rxjs/Rx';import 'rxjs/add/operator/map';
import {  map } from 'rxjs/operators';
import 'rxjs/add/operator/catch';
import { Observable } from 'rxjs';
import { Root } from '../shared/Root';

@Injectable()
export class SalesDataService {

  constructor(private _http: HttpClient) { }
  root:Root;
  getOrders(pageIndex: number, pageSize: number) { 
    this._http.get('https://localhost:5001/api/order/' + pageIndex + '/' + pageSize).toPromise().then(
     res=>{
      this.root = res as Root;
    }
  );
  }

  getOrdersByCustomer(n: number) {
    return this._http.get('https://localhost:5001/api/order/bycustomer/' + n).pipe(
        map((response: any) => response.json));
  }

  getOrdersByState() {
    return this._http.get('https://localhost:5001/api/order/bystate/').pipe(
        map((response: any) => response.json));
  }
  
  delay(ms: number) {
    return new Promise(resolve => setTimeout(resolve, ms));
  }
}
