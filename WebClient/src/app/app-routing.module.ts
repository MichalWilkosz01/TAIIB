import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductsComponent } from './products/products.component';
import { OrdersComponent } from './orders/orders.component';
import { AllComponent } from './orders/all/all.component';
import { BasketComponent } from './basket/basket.component';
import { DetailsComponent } from './products/details/details.component';

const routes: Routes = [
  { path: 'products', component: ProductsComponent },
  { path: 'product/:id', component: DetailsComponent },
  { path: 'orders', component: OrdersComponent },
  { path: 'orders/all', component: AllComponent },
  { path: 'basket', component: BasketComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
