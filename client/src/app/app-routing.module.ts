import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MarketplaceComponent } from './marketplace/marketplace.component';
import { OrdersComponent } from './orders/orders.component';
import { RegisterComponent } from './register/register.component';

const routes: Routes = [
{path: '', component: MarketplaceComponent},
{path: 'orders', component: OrdersComponent},
{path: 'register', component: RegisterComponent},
{path: '**', component: MarketplaceComponent, pathMatch: 'full'},//route to the main page if page not found
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
