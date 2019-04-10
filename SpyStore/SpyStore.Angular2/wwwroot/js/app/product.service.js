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
const app_config_1 = require("./app.config");
let ProductService = class ProductService {
    constructor(http, config) {
        this.http = http;
        this.config = config;
    }
    getFeaturedProducts() {
        return this.http.get(this.config.apiEndPoint + "product/featured")
            .map(response => response.json());
    }
    getCategory(id) {
        return this.http.get(this.config.apiEndPoint + "category/" + id)
            .map(response => response.json());
    }
    getProductsForACategory(id) {
        return this.http.get(this.config.apiEndPoint + "category/" + id + "/products")
            .map(response => response.json());
    }
    searchProduct(searchText) {
        return this.http.get(this.config.apiEndPoint + "search/" + searchText)
            .map(response => response.json());
    }
};
ProductService = __decorate([
    core_1.Injectable(),
    __param(1, core_1.Inject(app_config_1.APP_CONFIG)),
    __metadata("design:paramtypes", [http_1.Http, Object])
], ProductService);
exports.ProductService = ProductService;
//# sourceMappingURL=product.service.js.map