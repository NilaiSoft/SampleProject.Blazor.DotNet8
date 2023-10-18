import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CustomerComponent } from './customer/customer.component';
import { ProductListComponent } from './Products/product-list/product-list.component';

const appRoutes: Routes = [
  { path: './customer/customer', component: CustomerComponent },
  { path: '/Products/product-list/product-list', component: ProductListComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(appRoutes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
