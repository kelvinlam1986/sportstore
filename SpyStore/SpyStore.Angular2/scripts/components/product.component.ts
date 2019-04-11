import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router, RouterLink } from '@angular/router';
import { ProductService } from '../product.service';
import { CartService } from '../cart.service';
import { UserService } from '../user.service';
import { LoggingService } from '../logging.service';

@Component({
    templateUrl: 'app/components/productDetail.component.html'
})
export class ProductDetailComponent implements OnInit {

    message: string;
    isAuthenticated: Boolean;
    product: any;
    quantiy: any = 1;

    constructor(
        private _router: Router,
        private _route: ActivatedRoute,
        private _productService: ProductService,
        private _cartService: CartService,
        private _userService: UserService,
        private _loggingService: LoggingService) {
        this.product = {};
        this.isAuthenticated = this._userService.IsAuthenticated;
    }

    ngOnInit() {
        this.isAuthenticated = this._userService.IsAuthenticated;
        this._userService.ensureAuthenticated().subscribe(_ => {
            this.isAuthenticated = true;
        });

        this._route.params.subscribe((params: Params) => {
            let id: number = +params['id'];
            this._productService.getProduct(id)
                .subscribe(product => {
                    this.product = product
                }, err => {
                    this._loggingService.logError('Error loading product', err);
                })
        })
    }

    addToCart() {
        this._cartService.addToCart({
            ProductId: this.product.Id,
            Quantity: this.quantiy
        }).subscribe((response) => {
            if (response.status == 201) {
                this._router.navigate(['/cart']);
            } else {
                this._loggingService.log(response.statusText);
                }
            }, err => this._loggingService.logError('Error Adding to Cart', err));
    }
}