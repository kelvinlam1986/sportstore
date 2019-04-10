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
const router_1 = require("@angular/router");
const product_service_1 = require("../product.service");
const logging_service_1 = require("../logging.service");
let ProductsComponent = class ProductsComponent {
    constructor(_route, _service, _loggingService) {
        this._route = _route;
        this._service = _service;
        this._loggingService = _loggingService;
    }
    ngOnInit() {
        this._route.params.subscribe(params => {
            if ("categoryId" in params) {
                let categoryId = +params["categoryId"];
                this._service.getCategory(categoryId).subscribe(category => this.header = category.CategoryName, err => this._loggingService.logError("Error loading Category", err));
                this._service.getProductsForACategory(categoryId)
                    .subscribe(products => {
                    this.products = products;
                }, err => this._loggingService.logError('Error loading Products', err));
            }
            else if (!("searchText" in this._route.snapshot.queryParams)) {
                this.getFeaturedProducts();
            }
        });
        this._route.queryParams.subscribe(params => {
            if ("searchText" in params) {
                let searchText = params['searchText'];
                this.header = 'Search for: ' + searchText;
                this._service.searchProduct(searchText).subscribe(products => {
                    this.products = products;
                }, err => this._loggingService.logError('Error loading Products', err));
            }
            else if (!("categoryId" in this._route.snapshot.params)) {
                this.getFeaturedProducts();
            }
        });
    }
    getFeaturedProducts() {
        this._service.getFeaturedProducts().subscribe(products => {
            this.header = "Featured Products";
            this.products = products;
        }, err => this._loggingService.logError("Error loading Featured Products", err));
    }
};
ProductsComponent = __decorate([
    core_1.Component({
        templateUrl: 'app/components/products.html'
    }),
    __metadata("design:paramtypes", [router_1.ActivatedRoute, product_service_1.ProductService, logging_service_1.LoggingService])
], ProductsComponent);
exports.ProductsComponent = ProductsComponent;
//# sourceMappingURL=products.component.js.map