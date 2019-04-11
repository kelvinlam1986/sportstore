"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
const core_1 = require("@angular/core");
const http_1 = require("@angular/http");
const Rx_1 = require("rxjs/Rx");
require("rxjs/Rx");
require("rxjs/add/operator/map");
let UserService = class UserService {
    constructor(_http) {
        this._http = _http;
        this._user = { Id: 0 };
    }
    getUsers() {
        return this._http.get("http://localhost:40001/api/customer")
            .map(response => response.json());
    }
    ensureAuthenticated() {
        let observable = Rx_1.Observable.create((subscriber) => {
            if (this.IsAuthenticated) {
                subscriber.next(this.User);
            }
            else {
                this._http.get("http://localhost:40001/api/customer")
                    .map(response => response.json())
                    .subscribe(users => {
                    this.User = users[0];
                    subscriber.next(this.User);
                });
            }
        });
        return observable;
    }
    logout() {
        this._user = { Id: 0, FullName: '', EmailAddress: '' };
    }
    set User(user) {
        if (user) {
            this._user.Id = user.Id;
            this._user.FullName = user.FullName;
            this._user.EmailAddress = user.EmailAddress;
        }
    }
    get User() {
        return this._user;
    }
    get IsAuthenticated() {
        return this._user.Id > 0;
    }
};
UserService = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [http_1.Http])
], UserService);
exports.UserService = UserService;
//# sourceMappingURL=user.service.js.map