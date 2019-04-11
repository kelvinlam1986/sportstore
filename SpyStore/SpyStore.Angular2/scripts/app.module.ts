import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { DatepickerModule, BsDropdownModule } from 'ng2-bootstrap'
import { AppComponent } from './app.component'
import { AppRoutingModule, routedComponents } from './app.routing';
import { APP_CONFIG, SPYSTORE_CONFIG } from './app.config';
import { ProductService } from './product.service';
import { LoggingService } from './logging.service';
import { CartService } from './cart.service';
import { UserService } from './user.service';

@NgModule({
    imports: [
        BrowserModule,
        FormsModule,
        BsDropdownModule.forRoot(),
        DatepickerModule.forRoot(),
        HttpModule,
        AppRoutingModule
    ],
    declarations: [
        AppComponent,
        routedComponents
    ],
    providers: [
        CartService,
        UserService,
        LoggingService,
        ProductService,
        { provide: APP_CONFIG, useValue: SPYSTORE_CONFIG }
    ],
    bootstrap: [AppComponent]
})
export class AppModule {

}