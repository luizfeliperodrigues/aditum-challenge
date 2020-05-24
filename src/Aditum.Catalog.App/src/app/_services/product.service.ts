import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Product } from '../_models/Product';

@Injectable()
export class ProductService {
    baseURL = 'http://localhost:5000/api/1/catalog';

constructor(private http: HttpClient) { }

getAllProducts(): Observable<Product[]>{
    return this.http.get<Product[]>(`${this.baseURL}`);
}

getProductById(id: string): Observable<Product>{
    return this.http.get<Product>(`${this.baseURL}/${id}`);
}

postProduct(product: Product) {
    return this.http.post(this.baseURL, product);
}

putProduct(product: Product) {
    return this.http.put(`${this.baseURL}/${product.id}`, product);
}

deleteProduct(id: string) {
    return this.http.delete(`${this.baseURL}/${id}`);
}

}
