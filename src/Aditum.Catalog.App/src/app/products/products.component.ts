import { Component, OnInit } from '@angular/core';
import { ProductService } from '../_services/product.service';
import { Product } from '../_models/Product';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {
  title = 'Produtos';
  products: Product[];
  product: Product;
  registerForm: FormGroup;

  constructor(
      private productService: ProductService
    // , private toastr: ToastrService
  ) { }

  ngOnInit() {
    this.getProducts();
  }

  getProducts()
  {
    this.productService.getAllProducts().subscribe(
      (response: any) => {
        console.log(response);
        this.products = response.products;
      }, error => {
        // this.toastr.error(`Erro ao tentar carregar os produtos: ${error}`);
        console.log(error);
      }
    );
  }

}
