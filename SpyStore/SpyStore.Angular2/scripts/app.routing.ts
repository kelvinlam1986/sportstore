import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProductsComponent } from './components/products.component';
import { ProductDetailComponent } from './components/product.component';

const routes: Routes = [
    {
        path: '',
        redirectTo: '/products',
        pathMatch: 'full'
    },
    { path: 'products', component: ProductsComponent },
    { path: 'products/:categoryId', component: ProductsComponent },
    { path: 'product/:id', component: ProductDetailComponent }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }

export const routedComponents = [ProductsComponent, ProductDetailComponent];