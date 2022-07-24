import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Products } from '../_models/products';
import { ProductService } from '../_services/product.service';



@Component({
  selector: 'app-marketplace',
  templateUrl: './marketplace.component.html',
  styleUrls: ['./marketplace.component.scss']
})
export class MarketplaceComponent implements OnInit {
  products: Products[];

  constructor(private http: HttpClient, private productService: ProductService ) { }

  ngOnInit(): void {
    this.getProducts();
  }

  getProducts() {
    return this.productService.getProducts().subscribe(products => this.products = products);
  }




}
