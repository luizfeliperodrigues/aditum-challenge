import { Component, OnInit } from '@angular/core';
import { ProductService } from '../_services/product.service';
import { Product } from '../_models/Product';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';

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
  bodyDeleteProduct = '';

  infoSave = 'post';

  constructor(
        private productService: ProductService
      , private formBuilder: FormBuilder
    // , private toastr: ToastrService
  ) { }

  ngOnInit() {
    this.validation();
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

  validation(){
    this.registerForm = this.formBuilder.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
      amount: ['', [Validators.required, Validators.pattern('[1-9]*')]],
      weight: ['', Validators.pattern('[1-9]*')],
      hight: ['', Validators.pattern('[1-9]*')],
      width: ['', Validators.pattern('[1-9]*')]
    });
  }

  saveProduct(template: any){
    if(this.registerForm.valid){
      if (this.infoSave === 'post') {
        this.product = Object.assign({}, this.registerForm.value);
        this.productService.postProduct(this.product).subscribe(
          () => {
            template.hide();
            this.getProducts();
          }, error => {
            console.log(error);
          }
        );
      }

      if (this.infoSave === 'put') {
        this.product = Object.assign({}, this.registerForm.value);
        this.productService.putProduct(this.product).subscribe(
          () => {
            template.hide();
            this.getProducts();
          }, error => {
            console.log(error);
          }
        );
      }
    }
  }

  openModal(template: any){
    this.registerForm.reset();
    template.show();
  }

  newProduct(template: any){
    this.infoSave = 'post';
    this.openModal(template);
  }

  editProduct(product: Product, template: any){
    this.infoSave = 'put';
    this.openModal(template);
    this.product = product;
    this.registerForm.patchValue(product);
  }

  excludeProduct(product: Product, template: any) {
    this.openModal(template);
    this.product = product;
    this.bodyDeleteProduct = `Tem certeza que deseja excluir o produto: ${product.name}?`;
  }

  confirmDelete(template: any) {
    this.productService.deleteProduct(this.product.id).subscribe(
      () => {
          template.hide();
          this.getProducts();
        }, error => {
          console.log(error);
        }
    );
  }

}
