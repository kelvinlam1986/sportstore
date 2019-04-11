import { Injectable, Inject } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable, Observer } from 'rxjs/Rx';
import 'rxjs/Rx'
import 'rxjs/add/operator/map';
import { AppConfig, APP_CONFIG } from './app.config';

@Injectable()
export class ProductService {
    constructor(private http: Http, @Inject(APP_CONFIG) private config: AppConfig) {
    }

    getFeaturedProducts(): Observable<any> {
        return this.http.get(this.config.apiEndPoint + "product/featured")
            .map(response => response.json());
    }

    getCategory(id: number | string): Observable<any> {
        return this.http.get(this.config.apiEndPoint + "category/" + id)
            .map(response => response.json());
    }

    getProductsForACategory(id: number | string): Observable<any> {
        return this.http.get(this.config.apiEndPoint + "category/" + id + "/products")
            .map(response => response.json());
    }

    searchProduct(searchText: string): Observable<any> {
        return this.http.get(this.config.apiEndPoint + "search/" + searchText)
            .map(response => response.json());
    }

    getProduct(id: number | string): Observable<any> {
        return this.http.get(this.config.apiEndPoint + "product/" + id)
            .map(response => response.json());
    }
}