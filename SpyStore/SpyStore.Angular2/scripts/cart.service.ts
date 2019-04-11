import { Inject, Injectable } from '@angular/core';
import { Http, Headers, Response } from '@angular/http';
import { Observable, Observer } from 'rxjs/Rx';
import 'rxjs/Rx';
import 'rxjs/add/operator/map'
import { UserService } from './user.service';
import { AppConfig, APP_CONFIG } from './app.config'

export class Cart {
}

@Injectable()
export class CartService {
    constructor(private http: Http, private _userService: UserService, @Inject(APP_CONFIG) private config: AppConfig) {
    }

    addToCart(cartRecord: ShoppingCartRecord): Observable<Response> {
        var headers = new Headers();
        headers.append('Content-Type', 'application/json');
        return this.http.post(
            this.config.apiEndPoint + "shoppingcart/" + this._userService.User.Id,
            JSON.stringify(cartRecord),
            { headers: headers });
    }
}

export interface ShoppingCartRecord {
    Id?: number;
    CustomerId?: number;
    ProductId: number;
    Quantity: number;
    TimeStamp?: any;
    CurrentPrice?: number;
    LineItemTotal?: number;
}