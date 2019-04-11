﻿import { Injectable } from '@angular/core';
import { Http, Headers, Response } from '@angular/http';
import { Observable, Observer } from 'rxjs/Rx';
import 'rxjs/Rx';
import 'rxjs/add/operator/map';

export interface User {
    Id?: number,
    FullName?: string,
    EmailAddress?: string
}

@Injectable()
export class UserService {
    private _user: User = { Id: 0 };

    constructor(private _http: Http) {

    }

    getUsers(): Observable<User[]> {
        return this._http.get("http://localhost:40001/api/customer")
            .map(response => <User[]>response.json());
    }

    ensureAuthenticated(): Observable<User> {
        let observable: Observable<User> = Observable.create((subscriber) => {
            if (this.IsAuthenticated) {
                subscriber.next(this.User);
            } else {
                this._http.get("http://localhost:40001/api/customer")
                    .map(response => <User[]>response.json())
                    .subscribe(users => {
                        this.User = users[0];
                        subscriber.next(this.User);
                    })
            }
        });

        return observable;
    }

    logout() {
        this._user = { Id: 0, FullName: '', EmailAddress: '' }
    }

    set User(user: User) {
        if (user) {
            this._user.Id = user.Id;
            this._user.FullName = user.FullName;
            this._user.EmailAddress = user.EmailAddress;
        }
    }

    get User(): User {
        return this._user;
    }

    get IsAuthenticated(): boolean {
        return this._user.Id > 0;
    }
}