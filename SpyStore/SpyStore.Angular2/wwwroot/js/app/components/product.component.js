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
const cart_service_1 = require("../cart.service");
const user_service_1 = require("../user.service");
const logging_service_1 = require("../logging.service");
let ProductDetailComponent = class ProductDetailComponent {
    constructor(_router, _route, _productService, _cartService, _userService, _loggingService) {
        this._router = _router;
        this._route = _route;
        this._productService = _productService;
        this._cartService = _cartService;
        this._userService = _userService;
        this._loggingService = _loggingService;
        this.quantiy = 1;
        this.product = {};
        this.isAuthenticated = this._userService.IsAuthenticated;
    }
    ngOnInit() {
        this.isAuthenticated = this._userService.IsAuthenticated;
        this._userService.ensureAuthenticated().subscribe(_ => {
            this.isAuthenticated = true;
        });
        this._route.params.subscribe((params) => {
            let id = +params['id'];
            this._productService.getProduct(id)
                .subscribe(product => {
                this.product = product;
            }, err => {
                this._loggingService.logError('Error loading product', err);
            });
        });
    }
    addToCart() {
        this._cartService.addToCart({
            ProductId: this.product.Id,
            Quantity: this.quantiy
        }).subscribe((response) => {
            if (response.status == 201) {
                this._router.navigate(['/cart']);
            }
            else {
                this._loggingService.log(response.statusText);
            }
        }, err => this._loggingService.logError('Error Adding to Cart', err));
    }
};
ProductDetailComponent = __decorate([
    core_1.Component({
        templateUrl: 'app/components/productDetail.component.html'
    }),
    __metadata("design:paramtypes", [router_1.Router,
        router_1.ActivatedRoute,
        product_service_1.ProductService,
        cart_service_1.CartService,
        user_service_1.UserService,
        logging_service_1.LoggingService])
], ProductDetailComponent);
exports.ProductDetailComponent = ProductDetailComponent;
//# sourceMappingURL=product.component.js.map