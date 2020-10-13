import { Injectable } from '@angular/core';
import { HttpClient ,HttpHeaders} from '@angular/common/http';
import { Observable } from 'rxjs';
import 'rxjs/Rx';import 'rxjs/add/operator/map';
import { catchError, map } from 'rxjs/operators';
import { ServerMessage } from '../shared/server-message';
import { Server } from '../shared/server';
import 'rxjs/add/operator/catch';

@Injectable()
export class ServerService {

  // https://www.w3.org/Protocols/rfc2616/rfc2616-sec14.html#sec14.4

  constructor(private _http: HttpClient) {
    
  }

  getServers(): Observable<Server[]> {
    return this._http.get('https://localhost:5001/api/server').pipe(
        map((response: any) => response.json())).pipe(catchError(this.handleError));
  }

  handleError(error: any) {
    const errMsg = (error.message) ? error.message :
      error.status ? `${error.status} - ${error.statusText}` : 'Server error';

    console.log(errMsg);
    return Observable.throw(errMsg);
  }

  handleServerMessage(msg: ServerMessage): Observable<Response> {
    const options = {
        headers: new HttpHeaders().append('Content-Type', 'application/json')
        .append('Accept', 'q=0.8;application/json;q=0.9')
      }
    const url = 'https://localhost:5001/api/server/' + msg.id;
    return this._http.put(url, msg, options).pipe(map((response: any) => response.json()));
  }

}
