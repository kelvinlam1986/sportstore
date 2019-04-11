"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
const core_1 = require("@angular/core");
const platform_browser_1 = require("@angular/platform-browser");
const forms_1 = require("@angular/forms");
const http_1 = require("@angular/http");
const ng2_bootstrap_1 = require("ng2-bootstrap");
const app_component_1 = require("./app.component");
const app_routing_1 = require("./app.routing");
const app_config_1 = require("./app.config");
const product_service_1 = require("./product.service");
const logging_service_1 = require("./logging.service");
const cart_service_1 = require("./cart.service");
const user_service_1 = require("./user.service");
let AppModule = class AppModule {
};
AppModule = __decorate([
    core_1.NgModule({
        imports: [
            platform_browser_1.BrowserModule,
            forms_1.FormsModule,
            ng2_bootstrap_1.BsDropdownModule.forRoot(),
            ng2_bootstrap_1.DatepickerModule.forRoot(),
            http_1.HttpModule,
            app_routing_1.AppRoutingModule
        ],
        declarations: [
            app_component_1.AppComponent,
            app_routing_1.routedComponents
        ],
        providers: [
            cart_service_1.CartService,
            user_service_1.UserService,
            logging_service_1.LoggingService,
            product_service_1.ProductService,
            { provide: app_config_1.APP_CONFIG, useValue: app_config_1.SPYSTORE_CONFIG }
        ],
        bootstrap: [app_component_1.AppComponent]
    })
], AppModule);
exports.AppModule = AppModule;
//# sourceMappingURL=app.module.js.map