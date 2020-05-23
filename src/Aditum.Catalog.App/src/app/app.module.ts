import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';

import { ProductsComponent } from './products/products.component';
import { TitleComponent } from './_shared/title/title.component';
import { ProductService } from './_services/product.service';

@NgModule({
   declarations: [
      AppComponent,
      ProductsComponent,
      TitleComponent
   ],
   imports: [
      BrowserModule,
      AppRoutingModule,
      HttpClientModule
   ],
   providers: [
      ProductService
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
