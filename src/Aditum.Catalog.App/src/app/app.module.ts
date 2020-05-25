import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { ModalModule } from 'ngx-bootstrap/modal';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { AppRoutingModule } from './app-routing.module';
import { NgxPaginationModule } from 'ngx-pagination';
import { Ng2SearchPipeModule } from 'ng2-search-filter';

import { ProductService } from './_services/product.service';

import { AppComponent } from './app.component';
import { ProductsComponent } from './products/products.component';
import { TitleComponent } from './_shared/title/title.component';
import { NavComponent } from './nav/nav.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
   declarations: [
      AppComponent,
      ProductsComponent,
      TitleComponent,
      NavComponent
   ],
   imports: [
      BrowserModule,
      BsDropdownModule.forRoot(),
      TooltipModule.forRoot(),
      ModalModule.forRoot(),
      AppRoutingModule,
      HttpClientModule,
      FormsModule,
      ReactiveFormsModule,
      NgxPaginationModule,
      Ng2SearchPipeModule
   ],
   providers: [
      ProductService
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
