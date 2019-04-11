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
var __param = (this && this.__param) || function (paramIndex, decorator) {
    return function (target, key) { decorator(target, key, paramIndex); }
};
Object.defineProperty(exports, "__esModule", { value: true });
const core_1 = require("@angular/core");
const http_1 = require("@angular/http");
require("rxjs/Rx");
require("rxjs/add/operator/map");
const user_service_1 = require("./user.service");
const app_config_1 = require("./app.config");
class Cart {
}
exports.Cart = Cart;
let CartService = class CartService {
    constructor(http, _userService, config) {
        this.http = http;
        this._userService = _userService;
        this.config = config;
    }
    addToCart(cartRecord) {
        var headers = new http_1.Headers();
        headers.append('Content-Type', 'application/json');
        return this.http.post(this.config.apiEndPoint + "shoppingcart/" + this._userService.User.Id, JSON.stringify(cartRecord), { headers: headers });
    }
};
CartService = __decorate([
    core_1.Injectable(),
    __param(2, core_1.Inject(app_config_1.APP_CONFIG)),
    __metadata("design:paramtypes", [http_1.Http, user_service_1.UserService, Object])
], CartService);
exports.CartService = CartService;
//# sourceMappingURL=cart.service.js.map